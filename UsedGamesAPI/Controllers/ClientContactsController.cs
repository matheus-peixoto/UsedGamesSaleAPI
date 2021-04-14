using AutoMapper;
using ExthensionMethods.Object;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsedGamesAPI.DTOs.ClientContacts;
using UsedGamesAPI.Models;
using UsedGamesAPI.Repositories.Interfaces;

namespace UsedGamesAPI.Controllers
{
    [ApiController]
    [Route("clientcontacts")]
    public class ClientContactsController : ControllerBase
    {
        private readonly IClientContactRepository _clientContactRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientContactsController(IClientContactRepository clientContactRepository, IClientRepository clientRepository, IMapper mapper)
        {
            _clientContactRepository = clientContactRepository;
            _clientRepository = clientRepository;
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
        public async Task<ActionResult> Create([FromBody] CreateClientContactDTO sellerContactDTO)
        {
            await ValidateSellerContactModelForeignKeys(sellerContactDTO.ClientId);
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            ClientContact clientContact = _mapper.Map<ClientContact>(sellerContactDTO);
            await _clientContactRepository.CreateAsync(clientContact);

            return CreatedAtRoute("GetClientContactById", new { clientContact.Id }, clientContact);
        }

        [NonAction]
        public async Task ValidateSellerContactModelForeignKeys(int clientId)
        {
            if (!await _clientRepository.ExistsAsync(clientId))
                ModelState.AddModelError("ClientId", "The given client id does not corresponds to an existing client");
        }
    }
}
