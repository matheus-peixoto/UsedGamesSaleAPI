using ExthensionMethods.Object;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [Route("{id:int}", Name = "GetClientById")]
        public async Task<ActionResult<Client>> GetById([FromRoute] int id)
        {
            Client client = await _clientRepository.FindByIdAsync(id);
            if (client.IsNull()) return NotFound();

            return Ok(client);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Create([FromBody] Client client)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            await _clientRepository.CreateAsync(client);
            return CreatedAtRoute("GetClientById", new { client.Id }, client);
        }
    }
}
