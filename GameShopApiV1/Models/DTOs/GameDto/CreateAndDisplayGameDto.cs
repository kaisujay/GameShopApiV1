using System.ComponentModel.DataAnnotations;

namespace GameShopApiV1.Models.DTOs.GameDto
{
    public class CreateAndDisplayGameDto
    {
        [Required(ErrorMessage ="Name is Required")]
        [MaxLength(50)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Price is Required")]
        public float Price { get; set; }

        [Required(ErrorMessage = "Game Details is Required")]
        public string? GameDetails { get; set; }
    }
}
