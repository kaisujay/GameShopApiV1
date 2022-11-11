using Microsoft.AspNetCore.Identity;

namespace GameShopApiV1.Models
{
    public class PlayerModel : IdentityUser
    {
        public string? Name { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
