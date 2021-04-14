using ExthensionMethods.Object;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsedGamesAPI.Models;
using UsedGamesAPI.Repositories;
using UsedGamesAPI.Repositories.Interfaces;

namespace UsedGamesAPI.Controllers
{
    [ApiController]
    [Route("sellercontacts")]
    public class SellerContactsController : ControllerBase
    {
        private readonly ISellerContactRepository _sellerContactRepository;
        private readonly ISellerRespository _sellerRespository;

        public SellerContactsController(ISellerContactRepository sellerContactRepository, ISellerRespository sellerRespository)
        {
            _sellerContactRepository = sellerContactRepository;
            _sellerRespository = sellerRespository;
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

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Create([FromBody] SellerContact sellerContact)
        {
            await ValidateSellerContactModelForeignKeys(sellerContact.SellerId);
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            await _sellerContactRepository.CreateAsync(sellerContact);

            return Ok();
        }

        [NonAction]
        public async Task ValidateSellerContactModelForeignKeys(int sellerId)
        {
            if (!await _sellerRespository.ExistsAsync(sellerId))
                ModelState.AddModelError("SellerId", "The given seller id does not correspond to an existing seller");
        }
    }
}
