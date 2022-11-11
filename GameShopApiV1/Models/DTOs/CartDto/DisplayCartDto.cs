namespace GameShopApiV1.Models.DTOs.CartDto
{
    public class DisplayCartDto
    {
        public string? PlayerUserName { get; set; }
        public string? PlayerEmail { get; set; }
        public string? GameName { get; set; }
        public float GamePrice { get; set; }
        public string? GameDetails { get; set; }
    }
}
