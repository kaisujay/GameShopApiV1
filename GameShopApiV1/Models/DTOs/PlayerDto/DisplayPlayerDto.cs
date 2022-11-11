using System.ComponentModel.DataAnnotations;

namespace GameShopApiV1.Models.DTOs.PlayerDto
{
    public class DisplayPlayerDto
    {
        public string? PlayerId { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
