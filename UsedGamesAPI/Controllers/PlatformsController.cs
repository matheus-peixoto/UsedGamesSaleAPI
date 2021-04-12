using AutoMapper;
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
            List<Platform> platforms = await _platformRepository.FindAllAsync();
            return Ok(platforms);
        }

    }
}
