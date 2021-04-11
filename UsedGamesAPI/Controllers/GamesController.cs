using AutoMapper;
using ExthensionMethods.Object;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsedGamesAPI.Models;
using UsedGamesAPI.Repositories.Interfaces;

namespace UsedGamesAPI.Controllers
{
    [ApiController]
    [Route("games")]
    public class GamesController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlatformRepository _platformRepository;
        private readonly IMapper _mapper;

        public GamesController(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Game>>> Get()
        {
            List<Game> games = await _gameRepository.FindAllAsync();
            return Ok(games);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetGameById")]
        public async Task<ActionResult<Game>> GetById([FromRoute] int id)
        {
            Game game = await _gameRepository.FindByIdAsync(id);
            if (game.IsNull()) return NotFound();

            return Ok(game);
        }
    }
}
