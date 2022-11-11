using GameShopApiV1.Models.DTOs.CartDto;

namespace GameShopApiV1.Data.Repository
{
    public interface ICartRepository
    {
        Task<int> CreateCartAsync(CreateCartDto createCart);
        Task<IEnumerable<DisplayCartDto>> DisplayCartAsync(string playerId);
    }
}