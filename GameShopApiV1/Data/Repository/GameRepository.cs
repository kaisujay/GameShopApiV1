using GameShopApiV1.Models;
using GameShopApiV1.Models.DTOs.GameDto;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Xml.Linq;

namespace GameShopApiV1.Data.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly GameShopApiDbContext _shopApiDbContext;

        public GameRepository(GameShopApiDbContext shopApiDbContext)
        {
            _shopApiDbContext = shopApiDbContext;
        }

        public async Task<IEnumerable<CreateAndDisplayGameDto>> GetAllGamesAsync()
        {
            var allGamesList = await _shopApiDbContext.Games
                .Select(x => new CreateAndDisplayGameDto()
                {
                    Name = x.Name,
                    Price = x.Price,
                    GameDetails = x.GameDetails
                }).ToListAsync();

            return allGamesList;
        }

        public async Task<IEnumerable<CreateAndDisplayGameDto>> GetSearchedGamesAsync(string value)
        {
            var allGamesList = await _shopApiDbContext.Games
                .Where(x => x.Name.Contains(value))
                .Select(x => new CreateAndDisplayGameDto()
                {
                    Name = x.Name,
                    Price = x.Price,
                    GameDetails = x.GameDetails
                }).ToListAsync();

            return allGamesList;
        }

        public async Task<CreateAndDisplayGameDto> GetSearchedGameByIdAsync(int value)
        {
            var game = await _shopApiDbContext.Games
               .Where(x => x.Id == value)
               .Select(x => new CreateAndDisplayGameDto()
               {
                   Name = x.Name,
                   Price = x.Price,
                   GameDetails = x.GameDetails
               }).FirstOrDefaultAsync();

            return game;
        }

        public async Task<int> CreateGamesAsync(CreateAndDisplayGameDto createGame)
        {
            var game = new GameModel()
            {
                Name = createGame.Name,
                Price = createGame.Price,
                GameDetails = createGame.GameDetails
            };

            _shopApiDbContext.Games.Add(game);
            await _shopApiDbContext.SaveChangesAsync();
            return game.Id;
        }       
    }
}
