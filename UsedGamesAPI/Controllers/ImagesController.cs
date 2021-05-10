using AutoMapper;
using ExthensionMethods.Object;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsedGamesAPI.Models;
using UsedGamesAPI.Models.DTOs.Image;
using UsedGamesAPI.Repositories.Interfaces;
using UsedGamesAPI.Services.Filters;
using UsedGamesAPI.Services.Filters.ImageFilters;

namespace UsedGamesAPI.Controllers
{
    [ApiController]
    [Authorize(Roles = "Manager,Seller")]
    [Route("images")]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public ImagesController(IImageRepository imageRepository, IGameRepository gameRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Image>>> Get()
        {
            List<Image> images = await _imageRepository.FindAllAsync();
            return Ok(images);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetImageById")]
        public async Task<ActionResult<Image>> GetById([FromRoute] int id)
        {
            Image image = await _imageRepository.FindByIdAsync(id);
            if (image.IsNull()) return NotFound();

            return Ok(image);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Game>> Create([FromBody] CreateImageDTO imgaeDTO)
        {
            Image image = _mapper.Map<Image>(imgaeDTO);
            await _imageRepository.CreateAsync(image);

            return CreatedAtRoute("GetImageById", new { image.Id }, image);
        }

        [HttpPut]
        [Route("{id:int}")]
        [ValidateImageForeignKeysOnUpdate]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateImageDTO imageDTO)
        {
            Image image = await _imageRepository.FindByIdAsync(id);
            if (image.IsNull()) return NotFound();

            _mapper.Map(imageDTO, image);
            await _imageRepository.UpdateAsync(image);

            return NoContent();
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<ActionResult> UpdatePartial([FromRoute] int id, [FromBody] JsonPatchDocument<UpdateImageDTO> patchGameDTO)
        {
            Image image = await _imageRepository.FindByIdAsync(id);
            if (image.IsNull()) return NotFound();

            UpdateImageDTO imageDTO = _mapper.Map<UpdateImageDTO>(image);

            if (!TryValidateModel(imageDTO)) return ValidationProblem(ModelState);
            await ValidateImageModelForeignKeysOnPatch(patchGameDTO, imageDTO);
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            patchGameDTO.ApplyTo(imageDTO);
            _mapper.Map(imageDTO, image);
            await _imageRepository.UpdateAsync(image);

            return NoContent();
        }

        [NonAction]
        private async Task ValidateImageModelForeignKeysOnPatch(JsonPatchDocument<UpdateImageDTO> patchImageDTO, UpdateImageDTO imageDTO)
        {
            if (patchImageDTO.Operations.Any(op => op.path.ToLower() == "gameid") && !await _gameRepository.ExistsAsync(imageDTO.GameId))
            {
                ModelState.AddModelError("GameId", "The given game id does not corresponds to an existing game");
            }
        }
    }
}
