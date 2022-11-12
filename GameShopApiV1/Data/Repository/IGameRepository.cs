using GameShopApiV1.Models.DTOs.GameDto;

namespace GameShopApiV1.Data.Repository
{
    public interface IGameRepository
    {
        Task<int> CreateGamesAsync(GameDto createGame);
        Task<IEnumerable<GameDto>> GetAllGamesAsync();
        Task<IEnumerable<GameDto>> GetSearchedGamesAsync(string value);
        Task<GameDto> GetSearchedGameByIdAsync(int value);
    }
}