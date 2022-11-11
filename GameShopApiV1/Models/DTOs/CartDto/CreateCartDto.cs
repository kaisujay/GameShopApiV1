namespace GameShopApiV1.Models.DTOs.CartDto
{
    public class CreateCartDto
    {
        public string? PlayerId { get; set; }
        public int GameId { get; set; }
    }
}
