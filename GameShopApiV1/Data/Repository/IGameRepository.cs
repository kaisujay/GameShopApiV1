using GameShopApiV1.Models.DTOs.GameDto;

namespace GameShopApiV1.Data.Repository
{
    public interface IGameRepository
    {
        Task<int> CreateGamesAsync(CreateAndDisplayGameDto createGame);
        Task<IEnumerable<CreateAndDisplayGameDto>> GetAllGamesAsync();
        Task<IEnumerable<CreateAndDisplayGameDto>> GetSearchedGamesAsync(string value);
    }
}