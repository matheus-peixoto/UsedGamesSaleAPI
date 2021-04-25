using AutoMapper;
using ExthensionMethods.Object;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsedGamesAPI.DTOs.Manager;
using UsedGamesAPI.DTOs.Users;
using UsedGamesAPI.Models;
using UsedGamesAPI.Models.Enums;
using UsedGamesAPI.Repositories.Interfaces;
using UsedGamesAPI.Services;

namespace UsedGamesAPI.Controllers
{
    [ApiController]
    [Authorize(Roles = "Manager")]
    [Route("managers")]
    public class ManagersController : ControllerBase
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IMapper _mapper;

        public ManagersController(IManagerRepository managerRepository, IMapper mapper)
        {
            _managerRepository = managerRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Authenticate(UserDTO userDTO)
        {
            Manager manager = await _managerRepository.FindByAccountAsync(userDTO.Email, userDTO.Password);
            if (manager.IsNull()) return NotFound(new { message = "Invalid user or password" });

            string token = TokenService.GenerateToken(manager, AccountType.Manager);
            manager.Password = "";
            return Ok(new { manager, token });
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Manager>>> Get()
        {
            List<Manager> managers = await _managerRepository.FindAllAsync();
            return Ok(managers);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetManagerById")]
        public async Task<ActionResult<Manager>> GetById([FromRoute] int id)
        {
            Manager manager = await _managerRepository.FindByIdAsync(id);
            if (manager.IsNull()) return NotFound();

            return Ok(manager);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Create([FromBody] CreateManagerDTO managerDTO)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            Manager manager = _mapper.Map<Manager>(managerDTO);
            await _managerRepository.CreateAsync(manager);

            return CreatedAtRoute("GetManagerById", new { manager.Id }, manager);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateManagerDTO managerDTO)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            Manager manager = await _managerRepository.FindByIdAsync(id);
            if (manager.IsNull()) return NotFound();

            _mapper.Map(managerDTO, manager);
            await _managerRepository.UpdateAsync(manager);

            return NoContent();
        }
    }
}
