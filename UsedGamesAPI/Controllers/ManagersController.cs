using ExthensionMethods.Object;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsedGamesAPI.DTOs.Users;
using UsedGamesAPI.Models;
using UsedGamesAPI.Repositories.Interfaces;
using UsedGamesAPI.Services;

namespace UsedGamesAPI.Controllers
{
    [ApiController]
    [Route("managers")]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerRepository _managerRepository;

        public ManagerController(IManagerRepository managerRepository)
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

            string token = TokenService.GenerateToken(manager);
            manager.Password = "";
            return Ok(new { manager, token });
        }
    }
}
