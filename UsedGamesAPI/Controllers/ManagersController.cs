using ExthensionMethods.Object;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public ManagersController(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
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
    }
}
