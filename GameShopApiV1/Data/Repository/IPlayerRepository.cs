using GameShopApiV1.Models.DTOs.PlayerDto;
using Microsoft.AspNetCore.Identity;

namespace GameShopApiV1.Data.Repository
{
    public interface IPlayerRepository
    {
        Task<string> CreatePlayerAsync(RegisterPlayerDto registerPlayer);
        Task<DisplayPlayerDto> GetPlayerDetailsByUserNameAsync(string userName);
        Task<DisplayPlayerDto> GetPlayerDetailsByIdAsync(string id);
        Task<string> LogInPlayerAsync(LogInPlayerDto logInPlayer);
        Task LogOutPlayerAsync();
    }
}