using GameShopApiV1.Data.Repository;
using GameShopApiV1.Models.DTOs.GameDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameShopApiV1.Controllers
{
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
        [Route("SearchedGames/{value}")]
        public async Task<IActionResult> GetSearchedGamesAsync(string value)
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
    }
}
