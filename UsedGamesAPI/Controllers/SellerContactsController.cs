using AutoMapper;
using ExthensionMethods.Object;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsedGamesAPI.DTOs.SellerContacts;
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
        private readonly IMapper _mapper;

        public SellerContactsController(ISellerContactRepository sellerContactRepository, ISellerRespository sellerRespository, IMapper mapper)
        {
            _sellerContactRepository = sellerContactRepository;
            _sellerRespository = sellerRespository;
            _mapper = mapper;
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
        public async Task<ActionResult> Create([FromBody] CreateSellerContactDTO sellerContactDTO)
        {
            await ValidateSellerContactModelForeignKeys(sellerContactDTO.SellerId);
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            SellerContact sellerContact = _mapper.Map<SellerContact>(sellerContactDTO);
            await _sellerContactRepository.CreateAsync(sellerContact);

            return Ok();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateSellerContactDTO sellerContactDTO)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            SellerContact sellerContact = await _sellerContactRepository.FindByIdAsync(id);
            if (sellerContact.IsNull()) return NotFound();

            _mapper.Map(sellerContactDTO, sellerContact);
            await _sellerContactRepository.UpdateAsync(sellerContact);

            return NoContent();
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<ActionResult> UpdatePartial([FromRoute] int id, [FromBody] JsonPatchDocument<UpdateSellerContactDTO> pacthSellerContactDTO)
        {
            SellerContact sellerContact = await _sellerContactRepository.FindByIdAsync(id);
            if (sellerContact.IsNull()) return NotFound();

            UpdateSellerContactDTO sellerDTO = _mapper.Map<UpdateSellerContactDTO>(sellerContact);
            pacthSellerContactDTO.ApplyTo(sellerDTO);
            if (!TryValidateModel(sellerDTO)) return ValidationProblem(ModelState);

            _mapper.Map(sellerDTO, sellerContact);
            await _sellerContactRepository.UpdateAsync(sellerContact);

            return NoContent();
        }

        [NonAction]
        public async Task ValidateSellerContactModelForeignKeys(int sellerId)
        {
            if (!await _sellerRespository.ExistsAsync(sellerId))
                ModelState.AddModelError("SellerId", "The given seller id does not correspond to an existing seller");
        }
    }
}
