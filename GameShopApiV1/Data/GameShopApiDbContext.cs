using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameShopApiV1.Data
{
    public class GameShopApiDbContext : IdentityDbContext
    {
        public GameShopApiDbContext(DbContextOptions<GameShopApiDbContext> options) : base(options)
        {

        }
    }
}
