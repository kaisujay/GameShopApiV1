using GameShopApiV1.Data;
using GameShopApiV1.Data.Repository;
using GameShopApiV1.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region DbContext
builder.Services.AddDbContext<GameShopApiDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("GameShopApiDbConnnectionString"));
});
#endregion

#region AddedDbContextWithUserAndRole
builder.Services.AddIdentity<PlayerModel, IdentityRole>()
    .AddEntityFrameworkStores<GameShopApiDbContext>()
    .AddDefaultTokenProviders();
#endregion

#region JWTSetUp
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(option =>
    {
        option.SaveToken = true;
        option.RequireHttpsMetadata = false;
        option.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
    });
#endregion

#region IdentityPasswordSettingOverride
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 2;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;   
    options.Password.RequireUppercase = false;
});
#endregion

#region Dependencys
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
#endregion

#region CORSPolicyForAngularApp
builder.Services.AddCors(option =>
{
    /* This "AddDefaultPolicy" is a default setting from Microsoft */
    //option.AddDefaultPolicy(
    //        builder => builder
    //        .AllowAnyOrigin()         //We can also Specify the "Origin" with "WithOrigins" like - .WithOrigins("http://localhost:4200")
    //        .AllowAnyHeader());    //We can also Specify the "Header" with "WithHeader"
    //        .AllowAnyMethod()      //We can also Specify the "Method" with "WithMethod"

    /* This "AddPolicy" is a custome setting */
    option.AddPolicy("customCors",
        builder => builder
        .WithOrigins("http://localhost:4200")       //This is the Angular App I am using
        .AllowAnyHeader()
        .AllowAnyMethod()
        );
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();  // This is for "AddDefaultPolicy"
app.UseCors("customCors");

app.UseAuthentication();    //Added Because using Identity/Jwt
app.UseAuthorization();

app.MapControllers();

app.Run();
