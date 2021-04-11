using AutoMapper;
using ExthensionMethods.Object;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsedGamesAPI.DTOs.Clients;
using UsedGamesAPI.Models;
using UsedGamesAPI.Repository.Interfaces;

namespace UsedGamesAPI.Controllers
{
    [ApiController]
    [Route("clients")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientsController(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
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

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Create([FromBody] CreateClientDTO clientDTO)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            Client client = _mapper.Map<Client>(clientDTO);

            await _clientRepository.CreateAsync(client);

            return CreatedAtRoute("GetClientsById", new { client.Id }, client);
        }


        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateClientDTO clientDTO)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            Client client = await _clientRepository.FindByIdAsync(id);
            if (client.IsNull()) return NotFound();

            _mapper.Map(clientDTO, client);
            await _clientRepository.UpdateAsync(client);

            return NoContent();
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<ActionResult> UpdatePartial([FromRoute] int id, [FromBody] JsonPatchDocument<UpdateClientDTO> patchClientDTO)
        {
            Client client = await _clientRepository.FindByIdAsync(id);
            if (client.IsNull()) return NotFound();

            UpdateClientDTO clientDTO = _mapper.Map<UpdateClientDTO>(client);
            patchClientDTO.ApplyTo(clientDTO);

            if (!TryValidateModel(clientDTO)) return ValidationProblem(ModelState);

            _mapper.Map(clientDTO, client);
            await _clientRepository.UpdateAsync(client);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            Client client = await _clientRepository.FindByIdAsync(id);
            if (client.IsNull()) return NotFound();

            await _clientRepository.DeleteAsync(client);

            return NoContent();
        }
    }
}
