using AutoMapper;
using ExthensionMethods.Object;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsedGamesAPI.DTOs.ClientContacts;
using UsedGamesAPI.Models;
using UsedGamesAPI.Repositories.Interfaces;
using UsedGamesAPI.Services.Filters;
using UsedGamesAPI.Services.Filters.ClientFilters;

namespace UsedGamesAPI.Controllers
{
    [ApiController]
    [Route("clientcontacts")]
    public class ClientContactsController : ControllerBase
    {
        private readonly IClientContactRepository _clientContactRepository;
        private readonly IMapper _mapper;

        public ClientContactsController(IClientContactRepository clientContactRepository, IMapper mapper)
        {
            _clientContactRepository = clientContactRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<ClientContact>>> Get()
        {
            List<ClientContact> clientContacts = await _clientContactRepository.FindAllAsync();
            return Ok(clientContacts);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetClientContactById")]
        public async Task<ActionResult<ClientContact>> GetById([FromRoute] int id)
        {
            ClientContact clientContact = await _clientContactRepository.FindByIdAsync(id);
            if (clientContact.IsNull()) return NotFound();

            return Ok(clientContact);
        }

        [HttpPost]
        [Route("")]
        [ValidateClientContactForeignKeysOnCreate]
        public async Task<ActionResult> Create([FromBody] CreateClientContactDTO clientContactDTO)
        {
            ClientContact clientContact = _mapper.Map<ClientContact>(clientContactDTO);
            await _clientContactRepository.CreateAsync(clientContact);

            return CreatedAtRoute("GetClientContactById", new { clientContact.Id }, clientContact);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateClientContactDTO clientContactDTO)
        {
            ClientContact clientContact = await _clientContactRepository.FindByIdAsync(id);
            if (clientContact.IsNull()) return NotFound();

            _mapper.Map(clientContactDTO, clientContact);
            await _clientContactRepository.UpdateAsync(clientContact);

            return NoContent();
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<ActionResult> UpdatePartial([FromRoute] int id, [FromBody] JsonPatchDocument<UpdateClientContactDTO> pacthClientContactDTO)
        {
            ClientContact clientContact = await _clientContactRepository.FindByIdAsync(id);
            if (clientContact.IsNull()) return NotFound();

            UpdateClientContactDTO clientDTO = _mapper.Map<UpdateClientContactDTO>(clientContact);
            pacthClientContactDTO.ApplyTo(clientDTO);
            if (!TryValidateModel(clientDTO)) return ValidationProblem(ModelState);

            _mapper.Map(clientDTO, clientContact);
            await _clientContactRepository.UpdateAsync(clientContact);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            ClientContact clientContact = await _clientContactRepository.FindByIdAsync(id);
            if (clientContact.IsNull()) return NotFound();

            await _clientContactRepository.DeleteAsync(clientContact);

            return Ok(clientContact);
        }
    }
}
