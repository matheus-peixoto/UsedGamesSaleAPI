using AutoMapper;
using ExthensionMethods.Object;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsedGamesAPI.DTOs.SellerContacts;
using UsedGamesAPI.Models;
using UsedGamesAPI.Repositories.Interfaces;
using UsedGamesAPI.Services.Filters;

namespace UsedGamesAPI.Controllers
{
    [ApiController]
    [Route("sellercontacts")]
    public class SellerContactsController : ControllerBase
    {
        private readonly ISellerContactRepository _sellerContactRepository;
        private readonly IMapper _mapper;

        public SellerContactsController(ISellerContactRepository sellerContactRepository, IMapper mapper)
        {
            _sellerContactRepository = sellerContactRepository;
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
        [ValidateSellerContactForeignKeysOnCreate]
        public async Task<ActionResult> Create([FromBody] CreateSellerContactDTO sellerContactDTO)
        {
            SellerContact sellerContact = _mapper.Map<SellerContact>(sellerContactDTO);
            await _sellerContactRepository.CreateAsync(sellerContact);

            return CreatedAtRoute("GetSellerContactById", new { sellerContact.Id}, sellerContact);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateSellerContactDTO sellerContactDTO)
        {
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

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            SellerContact sellerContact = await _sellerContactRepository.FindByIdAsync(id);
            if (sellerContact.IsNull()) return NotFound();

            await _sellerContactRepository.DeleteAsync(sellerContact);

            return Ok(sellerContact);
        }
    }
}
