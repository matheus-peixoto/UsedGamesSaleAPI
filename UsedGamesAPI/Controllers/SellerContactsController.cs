using ExthensionMethods.Object;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsedGamesAPI.Models;
using UsedGamesAPI.Repositories.Interfaces;

namespace UsedGamesAPI.Controllers
{
    [ApiController]
    [Route("sellercontacts")]
    public class SellerContactsController : ControllerBase
    {
        private readonly ISellerContactRepository _sellerContactRepository;

        public SellerContactsController(ISellerContactRepository sellerContactRepository)
        {
            _sellerContactRepository = sellerContactRepository;
        }

        public async Task<ActionResult<List<SellerContact>>> Get()
        {
            List<SellerContact> sellerContacts = await _sellerContactRepository.FindAllAsync();
            return Ok(sellerContacts);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetSellerContactById")]
        public async Task<ActionResult<SellerContact>> GetById([FromRoute] int id)
        {
            SellerContact sellerContact = await _sellerContactRepository.FindByIdAsync(id);
            if (sellerContact.IsNull()) return NotFound();

            return Ok(sellerContact);
        }
    }
}
