using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsedGamesAPI.Libraries.ExthensionMethods.Object;
using UsedGamesAPI.Models;
using UsedGamesAPI.Repository.Interfaces;

namespace UsedGamesAPI.Controllers
{
    [ApiController]
    [Route("clients")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientsController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Client>>> Get()
        {
            List<Client> clients = await _clientRepository.FindAllAsync();
            return Ok(clients);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetClientsById")]
        public async Task<ActionResult<Client>> GetById([FromRoute] int id)
        {
            Client client = await _clientRepository.FindByIdAsync(id);
            if (client.IsNull()) return NotFound();

            return Ok(client);
        }
    }
}
