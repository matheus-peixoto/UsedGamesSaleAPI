using AutoMapper;
using ExthensionMethods.Object;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<ActionResult<List<ClientContact>>> Get()
        {
            List<ClientContact> clientContacts = await _clientContactRepository.FindAllAsync();
            return Ok(clientContacts);
        }
    }
}
