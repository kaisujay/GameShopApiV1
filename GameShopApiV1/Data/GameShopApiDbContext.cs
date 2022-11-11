using GameShopApiV1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameShopApiV1.Data
{
    public class GameShopApiDbContext : IdentityDbContext<PlayerModel>
    {
        public GameShopApiDbContext(DbContextOptions<GameShopApiDbContext> options) : base(options)
        {

        }

        public DbSet<PlayerModel> Players { get; set; }
        public DbSet<GameModel> Games { get; set; }
    }
}
