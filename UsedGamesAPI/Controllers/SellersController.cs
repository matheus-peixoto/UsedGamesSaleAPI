using AutoMapper;
using ExthensionMethods.Object;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsedGamesAPI.DTOs.Sellers;
using UsedGamesAPI.Models;
using UsedGamesAPI.Repositories.Interfaces;

namespace UsedGamesAPI.Controllers
{
    [ApiController]
    [Route("sellers")]
    public class SellersController : ControllerBase
    {
        private readonly ISellerRespository _sellerRespository;
        private readonly IMapper _mapper;

        public SellersController(ISellerRespository sellerRespository, IMapper mapper)
        {
            _sellerRespository = sellerRespository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Seller>>> Get()
        {
            List<Seller> sellers = await _sellerRespository.FindAllAsync();
            return Ok(sellers);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetSellerById")]
        public async Task<ActionResult<Seller>> GetById([FromRoute] int id)
        {
            Seller seller = await _sellerRespository.FindByIdAsync(id);
            if (seller.IsNull()) return NotFound();

            return Ok(seller);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Create([FromBody] CreateSellerDTO sellerDTO)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            Seller seller = _mapper.Map<Seller>(sellerDTO);
            await _sellerRespository.CreateAsync(seller);

            return CreatedAtRoute("GetSellerById", new { seller.Id }, seller);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateSellerDTO sellerDTO)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            Seller seller = await _sellerRespository.FindByIdAsync(id);
            if (seller.IsNull()) return NotFound();

            _mapper.Map(sellerDTO, seller);
            await _sellerRespository.UpdateAsync(seller);

            return NoContent();
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<ActionResult> UpdatePartial([FromRoute] int id, [FromBody] JsonPatchDocument<UpdateSellerDTO> pacthSellerDTO)
        {
            Seller seller = await _sellerRespository.FindByIdAsync(id);
            if (seller.IsNull()) return NotFound();

            UpdateSellerDTO sellerDTO = _mapper.Map<UpdateSellerDTO>(seller);
            pacthSellerDTO.ApplyTo(sellerDTO);
            if (!TryValidateModel(sellerDTO)) return ValidationProblem(ModelState);

            _mapper.Map(sellerDTO, seller);
            await _sellerRespository.UpdateAsync(seller);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            Seller seller = await _sellerRespository.FindByIdAsync(id);
            if (seller.IsNull()) return NotFound();

            await _sellerRespository.DeleteAsync(seller);

            return NoContent();
        }
    }
}
