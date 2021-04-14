using AutoMapper;
using ExthensionMethods.Object;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsedGamesAPI.DTOs.Platforms;
using UsedGamesAPI.Models;
using UsedGamesAPI.Repositories.Interfaces;

namespace UsedGamesAPI.Controllers
{
    [ApiController]
    [Route("platforms")]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepository _platformRepository;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepository platformRepository, IMapper mapper)
        {
            _platformRepository = platformRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Platform>>> Get()
        {
            List<Platform> platforms = await _platformRepository.FindAllWithGamesAsync();
            return Ok(platforms);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetPlatformById")]
        public async Task<ActionResult<Platform>> GetById([FromRoute] int id)
        {
            Platform platform = await _platformRepository.FindByIdWithGamesAsync(id);
            if (platform.IsNull()) return NotFound();

            return Ok(platform);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Create([FromBody] CreateUpdatePlatformDTO platformDTO)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            Platform platform = _mapper.Map<Platform>(platformDTO);
            await _platformRepository.CreateAsync(platform);

            return CreatedAtRoute("GetPlatformById", new { platform.Id }, platform);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] CreateUpdatePlatformDTO platformDTO)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            Platform platform = await _platformRepository.FindByIdAsync(id);
            if (platform.IsNull()) return NotFound();

             _mapper.Map(platformDTO, platform);
            await _platformRepository.UpdateAsync(platform);

            return NoContent();
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<ActionResult> UpdatePartial([FromRoute] int id, [FromBody] JsonPatchDocument<CreateUpdatePlatformDTO> patchPlatformDTO)
        {
            Platform platform = await _platformRepository.FindByIdAsync(id);
            if (platform.IsNull()) return NotFound();

            CreateUpdatePlatformDTO platformDTO = _mapper.Map<CreateUpdatePlatformDTO>(platform);
            patchPlatformDTO.ApplyTo(platformDTO);
            if (!TryValidateModel(platformDTO)) return ValidationProblem();

            _mapper.Map(platformDTO, platform);
            await _platformRepository.UpdateAsync(platform);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            Platform platform = await _platformRepository.FindByIdAsync(id);
            if (platform.IsNull()) return NotFound();

            await _platformRepository.DeleteAsync(platform);

            return NoContent();
        }
    }
}
