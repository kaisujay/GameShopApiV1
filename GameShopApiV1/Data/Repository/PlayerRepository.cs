using GameShopApiV1.Models;
using GameShopApiV1.Models.DTOs.PlayerDto;
using Microsoft.AspNetCore.Identity;

namespace GameShopApiV1.Data.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly UserManager<PlayerModel> _userManager;
        private readonly SignInManager<PlayerModel> _signInManager;

        public PlayerRepository(UserManager<PlayerModel> userManager, SignInManager<PlayerModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<string> CreatePlayerAsync(RegisterPlayerDto registerPlayer) //Return can be "Task<IdentityResult>" also
        {
            var newPlayer = new PlayerModel()
            {
                Name = registerPlayer.Name,
                UserName = registerPlayer.UserName,
                Email = registerPlayer.Email,
                DateOfBirth = registerPlayer.DateOfBirth,
            };

            await _userManager.CreateAsync(newPlayer, registerPlayer.Password);

            var newCreatedPlayerId = newPlayer.Id;
            return newCreatedPlayerId;
        }

        public async Task<DisplayPlayerDto> GetPlayerDetailsByUserNameAsync(string userName)
        {
            var outputPlayerModel = await _userManager.FindByNameAsync(userName);

            var player = new DisplayPlayerDto()
            {
                PlayerId =outputPlayerModel.Id,
                Name = outputPlayerModel.Name,
                UserName = outputPlayerModel.UserName,
                Email = outputPlayerModel.Email,
                DateOfBirth = outputPlayerModel.DateOfBirth,
            };
            return player;
        }

        public async Task<DisplayPlayerDto> GetPlayerDetailsByIdAsync(string id)
        {
            var outputPlayerModel = await _userManager.FindByIdAsync(id);

            var player = new DisplayPlayerDto()
            {
                PlayerId = outputPlayerModel.Id,
                Name = outputPlayerModel.Name,
                UserName = outputPlayerModel.UserName,
                Email = outputPlayerModel.Email,
                DateOfBirth = outputPlayerModel.DateOfBirth,
            };
            return player;
        }

        public async Task<SignInResult> LogInPlayerAsync(LogInPlayerDto logInPlayer)
        {
            var res = await _signInManager.PasswordSignInAsync(logInPlayer.UserName, logInPlayer.Password, false, false);
            return res;
        }

        public async Task LogOutPlayerAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
