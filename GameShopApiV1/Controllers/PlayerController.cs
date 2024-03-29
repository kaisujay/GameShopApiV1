﻿using GameShopApiV1.Data;
using GameShopApiV1.Data.Repository;
using GameShopApiV1.Models.DTOs.PlayerDto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GameShopApiV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerController(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreatePlayerAsync([FromBody]RegisterPlayerDto registerPlayer)
        {
            if(ModelState.IsValid)
            {
                var createdPlayerId = await _playerRepository.CreatePlayerAsync(registerPlayer);
                return StatusCode(201, createdPlayerId);
            }
            return BadRequest();
        }

        [HttpGet("ByUserName/{userName}")]
        public async Task<IActionResult> GetPlayerByUserNameAsync(string userName)
        {
            var player = await _playerRepository.GetPlayerDetailsByUserNameAsync(userName);
            return Ok(player);
        }

        [HttpGet("ById/{id}")]
        public async Task<IActionResult> GetPlayerByIdAsync(string id)
        {
            var player = await _playerRepository.GetPlayerDetailsByIdAsync(id);
            return Ok(player);
        }

        [HttpPost]  //This need to be a "post" because we are posting UserName and Password to API.
        [Route("LogIn")]
        public async Task<IActionResult> LogInPlayerAsync([FromBody]LogInPlayerDto logInPlayer)
        {
            if(ModelState.IsValid)
            {
                var res = await _playerRepository.LogInPlayerAsync(logInPlayer);
                if(string.IsNullOrEmpty(res))
                {
                    return Unauthorized(); ;
                }
                return Ok(res);
            }
            return BadRequest();
        }
    }
}
