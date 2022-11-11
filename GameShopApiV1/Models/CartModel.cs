using System.ComponentModel.DataAnnotations;

namespace GameShopApiV1.Models
{
    public class CartModel
    {
        [Key]
        public int Id { get; set; }
        public PlayerModel? Player { get; set; }
        public string? PlayerId { get; set; }
        public GameModel? Game { get; set; }
        public int GameId { get; set; }
    }
}
