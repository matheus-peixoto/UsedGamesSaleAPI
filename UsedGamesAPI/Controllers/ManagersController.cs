using AutoMapper;
using ExthensionMethods.Object;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
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
using UsedGamesAPI.Services.Token;

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
            return Ok(new { user = manager, token });
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
            Manager manager = _mapper.Map<Manager>(managerDTO);
            await _managerRepository.CreateAsync(manager);

            return CreatedAtRoute("GetManagerById", new { manager.Id }, manager);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateManagerDTO managerDTO)
        {
            Manager manager = await _managerRepository.FindByIdAsync(id);
            if (manager.IsNull()) return NotFound();

            _mapper.Map(managerDTO, manager);
            await _managerRepository.UpdateAsync(manager);

            return NoContent();
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<ActionResult> UpdatePartial([FromRoute] int id, [FromBody] JsonPatchDocument<UpdateManagerDTO> pacthSellerDTO)
        {
            Manager manager = await _managerRepository.FindByIdAsync(id);
            if (manager.IsNull()) return NotFound();

            UpdateManagerDTO managerDTO = _mapper.Map<UpdateManagerDTO>(manager);
            pacthSellerDTO.ApplyTo(managerDTO);
            if (!TryValidateModel(managerDTO)) return ValidationProblem(ModelState);

            _mapper.Map(managerDTO, manager);
            await _managerRepository.UpdateAsync(manager);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            Manager manager = await _managerRepository.FindByIdAsync(id);
            if (manager.IsNull()) return NotFound();

            await _managerRepository.DeleteAsync(manager);

            return Ok(manager);
        }
    }
}
