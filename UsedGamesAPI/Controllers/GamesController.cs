using AutoMapper;
using ExthensionMethods.Object;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsedGamesAPI.DTOs.Games;
using UsedGamesAPI.Models;
using UsedGamesAPI.Repositories.Interfaces;
using UsedGamesAPI.Services.Filters;

namespace UsedGamesAPI.Controllers
{
    [ApiController]
    [Route("games")]
    public class GamesController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IPlatformRepository _platformRepository;
        private readonly ISellerRepository _sellerRespository;
        private readonly IMapper _mapper;

        public GamesController(IGameRepository gameRepository, IImageRepository imageRepository, IPlatformRepository platformRepository, ISellerRepository sellerRespository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _imageRepository = imageRepository;
            _platformRepository = platformRepository;
            _sellerRespository = sellerRespository;
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

        [Authorize(Roles = "Manager,Seller")]
        [HttpGet]
        [Route("{id:int}/images", Name = "GetGameImagesById")]
        public async Task<ActionResult<List<Image>>> GetImages([FromRoute] int id)
        {
            List<Image> images = await _imageRepository.FindAllByGameAsync(id);
            return Ok(new { images });
        }

        [Authorize(Roles = "Seller")]
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Game>> Create([FromBody] CreateGameDTO gameDTO)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            Game game = _mapper.Map<Game>(gameDTO);
            await _gameRepository.CreateAsync(game);

            return CreatedAtRoute("GetGameById", new { game.Id }, game);
        }

        [Authorize(Roles = "Seller")]
        [HttpPut]
        [Route("{id:int}")]
        [ValidateGameForeignKeysOnUpdate]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateGameDTO gameDTO)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            Game game = await _gameRepository.FindByIdAsync(id);
            if (game.IsNull()) return NotFound();

            _mapper.Map(gameDTO, game);
            await _gameRepository.UpdateAsync(game);

            return NoContent();
        }

        [Authorize(Roles = "Seller")]
        [HttpPatch]
        [Route("{id:int}")]
        public async Task<ActionResult> UpdatePartial([FromRoute] int id, [FromBody] JsonPatchDocument<UpdateGameDTO> patchGameDTO)
        {
            Game game = await _gameRepository.FindByIdAsync(id);
            if (game.IsNull()) return NotFound();

            UpdateGameDTO gameDTO = _mapper.Map<UpdateGameDTO>(game);

            if (!TryValidateModel(gameDTO)) return ValidationProblem(ModelState);
            await ValidateGameModelForeignKeysOnPatch(patchGameDTO, gameDTO);
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            patchGameDTO.ApplyTo(gameDTO);
            _mapper.Map(gameDTO, game);
            await _gameRepository.UpdateAsync(game);

            return NoContent();
        }

        [HttpDelete]
        [Authorize(Roles = "Seller")]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            Game game = await _gameRepository.FindByIdAsync(id);
            if (game.IsNull()) return NotFound();

            await _gameRepository.DeleteAsync(game);

            return Ok(game);
        }

        [NonAction]
        private async Task ValidateGameModelForeignKeysOnPatch(JsonPatchDocument<UpdateGameDTO> patchGameDTO, UpdateGameDTO gameDTO)
        {
            if (patchGameDTO.Operations.Any(op => op.path.ToLower() == "platformid") && !await _platformRepository.ExistsAsync(gameDTO.PlatformId))
            {
                ModelState.AddModelError("PlatformId", "The given platform's id does not corresponds to an existing platform");
            }

            if (patchGameDTO.Operations.Any(op => op.path.ToLower() == "sellerid") && !await _sellerRespository.ExistsAsync(gameDTO.SellerId))
            {
                ModelState.AddModelError("SellerId", "The given seller id does not correspond to an existing seller");
            }
        }
    }
}
