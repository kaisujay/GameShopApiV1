using GameShopApiV1.Models;
using GameShopApiV1.Models.DTOs.PlayerDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GameShopApiV1.Data.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly UserManager<PlayerModel> _userManager;
        private readonly SignInManager<PlayerModel> _signInManager;
        private readonly IConfiguration _configuration;

        public PlayerRepository(UserManager<PlayerModel> userManager, SignInManager<PlayerModel> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<string> CreatePlayerAsync(RegisterPlayerDto registerPlayer) //Return can be "Task<IdentityResult>" also
        {
            var newPlayer = new PlayerModel()
            {
                Name = registerPlayer.Name,
                UserName = registerPlayer.UserName,
                Email = registerPlayer.Email,
                DateOfBirth = registerPlayer.DateOfBirth,
            };

            await _userManager.CreateAsync(newPlayer, registerPlayer.Password);
            await _userManager.AddToRoleAsync(newPlayer,"User");    //Adding a Role Manually
            var newCreatedPlayerId = newPlayer.Id;
            return newCreatedPlayerId;
        }

        public async Task<DisplayPlayerDto> GetPlayerDetailsByUserNameAsync(string userName)
        {
            var outputPlayerModel = await _userManager.FindByNameAsync(userName);

            var player = new DisplayPlayerDto()
            {
                PlayerId =outputPlayerModel.Id,
                Name = outputPlayerModel.Name,
                UserName = outputPlayerModel.UserName,
                Email = outputPlayerModel.Email,
                DateOfBirth = outputPlayerModel.DateOfBirth,
            };
            return player;
        }

        public async Task<DisplayPlayerDto> GetPlayerDetailsByIdAsync(string id)
        {
            var outputPlayerModel = await _userManager.FindByIdAsync(id);

            var player = new DisplayPlayerDto()
            {
                PlayerId = outputPlayerModel.Id,
                Name = outputPlayerModel.Name,
                UserName = outputPlayerModel.UserName,
                Email = outputPlayerModel.Email,
                DateOfBirth = outputPlayerModel.DateOfBirth,
            };
            return player;
        }

        public async Task<string> LogInPlayerAsync(LogInPlayerDto logInPlayer)
        {
            var res = await _signInManager.PasswordSignInAsync(logInPlayer.UserName, logInPlayer.Password, false, false);
            
            if(!res.Succeeded)
            {
                return null;
            }

            var loggedInPlayer = await _userManager.FindByNameAsync(logInPlayer.UserName);
            var loggedInPlayerRole = await _userManager.GetRolesAsync(loggedInPlayer);      //Getting THE Role

            var token = GenerateToken(logInPlayer, loggedInPlayerRole);
            return token;
        }

        private string GenerateToken(LogInPlayerDto logInPlayer, IList<string> loggedInPlayerRole)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, logInPlayer.UserName),
                new Claim(ClaimTypes.Role, loggedInPlayerRole[0]),    //Useing THE Role in JWT token generation
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var authSignInKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var authToken = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(authToken);
        }

        public async Task LogOutPlayerAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
