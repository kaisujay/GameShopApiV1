using GameShopApiV1.Models;
using GameShopApiV1.Models.DTOs.GameDto;
using Microsoft.AspNetCore.JsonPatch;
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

        public async Task<IEnumerable<GameDto>> GetAllGamesAsync()
        {
            var allGamesList = await _shopApiDbContext.Games
                .Select(x => new GameDto()
                {
                    Id=x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    GameDetails = x.GameDetails
                }).ToListAsync();

            return allGamesList;
        }

        public async Task<IEnumerable<GameDto>> GetSearchedGamesAsync(string value)
        {
            var allGamesList = await _shopApiDbContext.Games
                .Where(x => x.Name.Contains(value))
                .Select(x => new GameDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    GameDetails = x.GameDetails
                }).ToListAsync();

            return allGamesList;
        }

        public async Task<GameDto> GetSearchedGameByIdAsync(int value)
        {
            var game = await _shopApiDbContext.Games
               .Where(x => x.Id == value)
               .Select(x => new GameDto()
               {
                   Id = x.Id,
                   Name = x.Name,
                   Price = x.Price,
                   GameDetails = x.GameDetails
               }).FirstOrDefaultAsync();

            return game;
        }

        public async Task<int> CreateGamesAsync(GameDto createGame)
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

        public async Task<int> UpdateGameAsync(int gameId, GameDto updateGame)
        {
            #region Bad Approch : As it hits database two times
            //var getGame = await _shopApiDbContext.Games.Where(x => x.Id == gameId).FirstOrDefaultAsync(); //1st

            //if(getGame != null)
            //{
            //    getGame.Name = updateGame.Name;
            //    getGame.Price = updateGame.Price;
            //    getGame.GameDetails = updateGame.GameDetails;

            //    await _shopApiDbContext.SaveChangesAsync(); //2nd
            //}
            #endregion

            var game = new GameModel()
            {
                Id = gameId,
                Name = updateGame.Name,
                Price = updateGame.Price,
                GameDetails = updateGame.GameDetails
            };

            _shopApiDbContext.Games.Update(game);
            await _shopApiDbContext.SaveChangesAsync();
            return game.Id;
        }

        public async Task<int> UpdateGamePatchAsync(int gameId, JsonPatchDocument updateGame)
        {
            var getGame = await _shopApiDbContext.Games.Where(x => x.Id == gameId).FirstOrDefaultAsync();
            if (getGame != null)
            {
                updateGame.ApplyTo(getGame);
                await _shopApiDbContext.SaveChangesAsync();
            }
            return getGame.Id;
        }

        public async Task<string> DeleteGameAsync(int gameId)
        {
            //var getGame = await _shopApiDbContext.Games.Where(x => x.Id == gameId).FirstOrDefaultAsync();
            //OR
            var getGame = new GameModel()   // No Extra DB call so better
            { 
                Id = gameId 
            };

            _shopApiDbContext.Games.Remove(getGame);
            await _shopApiDbContext.SaveChangesAsync();

            return "Sucessfully Deleted";
        }
    }
}
