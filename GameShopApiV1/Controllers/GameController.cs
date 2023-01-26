using GameShopApiV1.Data.Repository;
using GameShopApiV1.Models.DTOs.GameDto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace GameShopApiV1.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;

        public GameController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGamesAsync()
        {
            var games = await _gameRepository.GetAllGamesAsync();
            if (games.Count() == 0)
            {
                return NotFound("No games found");
            }
            return Ok(games);
        }

        [HttpGet]
        [Route("SearchedGames")]
        public async Task<IActionResult> GetSearchedGamesAsync([FromQuery] string value)
        {
            var games = await _gameRepository.GetSearchedGamesAsync(value);
            if (games.Count() == 0)
            {
                return NotFound("No games found");
            }
            return Ok(games);
        }

        [HttpGet]
        [Route("SearchedGamesById/{value}")]
        public async Task<IActionResult> GetSearchedGamesByIdAsync(int value)
        {
            var games = await _gameRepository.GetSearchedGameByIdAsync(value);
            if (games == null)
            {
                return NotFound("No games found");
            }
            return Ok(games);
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateGameAsync([FromBody] GameDto createGame)
        {
            if (ModelState.IsValid)
            {
                var createdGamerId = await _gameRepository.CreateGamesAsync(createGame);
                return StatusCode(201, createdGamerId);
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("UpdateByPut")]
        public async Task<IActionResult> UpdateGameAsync([FromQuery] int gameId, [FromBody] GameDto updateGame)
        {
            if(ModelState.IsValid)
            {
                var updatedGame = await _gameRepository.UpdateGameAsync(gameId,updateGame);
                return Ok(updatedGame);
            }
            return BadRequest();
        }

        [HttpPatch]
        [Route("UpdateByPatch/{gameId}")]
        public async Task<IActionResult> UpdateGamepatchAsync([FromRoute] int gameId, [FromBody] JsonPatchDocument updateGame)
        {
            if (ModelState.IsValid)
            {
                var updatedGame = await _gameRepository.UpdateGamePatchAsync(gameId, updateGame);
                return Ok(updatedGame);
            }
            return BadRequest();

            #region Precess of Calling HttpPatch
            /*
             * In Postman
             * -----------------
                 * [ 
                        { 
                            "op": "replace", 
                            "path": "Name", 
                        "value": "FIFA NEW" 
                        }, 
                        {
                        "op": "remove", 
                        "path": "GameDetails" 
                        } 
                    ]       
             */

            #endregion
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteGameAsync([FromHeader] int gameId)
        {
            if (ModelState.IsValid)
            {
                var deleteGame = await _gameRepository.DeleteGameAsync(gameId);
                return Ok(deleteGame);
            }
            return BadRequest();
        }
    }
}
