using GameShopApiV1.Models.DTOs.GameDto;
using Microsoft.AspNetCore.JsonPatch;

namespace GameShopApiV1.Data.Repository
{
    public interface IGameRepository
    {
        Task<int> CreateGamesAsync(GameDto createGame);
        Task<IEnumerable<GameDto>> GetAllGamesAsync();
        Task<IEnumerable<GameDto>> GetSearchedGamesAsync(string value);
        Task<GameDto> GetSearchedGameByIdAsync(int value);
        Task<int> UpdateGameAsync(int gameId, GameDto updateGame);
        Task<int> UpdateGamePatchAsync(int gameId, JsonPatchDocument updateGame);
        Task<string> DeleteGameAsync(int gameId);
    }
}