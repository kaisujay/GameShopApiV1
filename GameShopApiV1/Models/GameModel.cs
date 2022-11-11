using System.ComponentModel.DataAnnotations;

namespace GameShopApiV1.Models
{
    public class GameModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public string? GameDetails { get; set; }
    }
}
