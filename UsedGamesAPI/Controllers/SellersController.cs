using AutoMapper;
using ExthensionMethods.Object;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using UsedGamesAPI.DTOs.Sellers;
using UsedGamesAPI.DTOs.Users;
using UsedGamesAPI.Models;
using UsedGamesAPI.Models.Enums;
using UsedGamesAPI.Repositories.Interfaces;
using UsedGamesAPI.Services.Token;

namespace UsedGamesAPI.Controllers
{
    [ApiController]
    [Route("sellers")]
    public class SellersController : ControllerBase
    {
        private readonly ISellerRepository _sellerRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public SellersController(ISellerRepository sellerRespository, IGameRepository gameRepository, IMapper mapper)
        {
            _sellerRepository = sellerRespository;
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Authenticate(UserDTO userDTO)
        {
            Seller seller = await _sellerRepository.FindByAccountAsync(userDTO.Email, userDTO.Password);
            if (seller.IsNull()) return NotFound(new { message = "Invalid user or password" });

            string token = TokenService.GenerateToken(seller, AccountType.Seller);
            seller.Password = "";
            return Ok(new { user = seller, token });
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Seller>>> Get()
        {
            List<Seller> sellers = await _sellerRepository.FindAllAsync();
            return Ok(sellers);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        [Route("{id:int}", Name = "GetSellerById")]
        public async Task<ActionResult<Seller>> GetById([FromRoute] int id)
        {
            Seller seller = await _sellerRepository.FindByIdAsync(id);
            if (seller.IsNull()) return NotFound();

            return Ok(seller);
        }

        [Authorize(Roles = "Seller,Manager")]
        [HttpGet]
        [Route("{id:int}/games", Name = "GetSellerGamesById")]
        public async Task<ActionResult<List<Game>>> GetGames([FromRoute] int id)
        {
            List<Game> games = await _gameRepository.FindAllBySellerAsync(id);
            return Ok(new { games });
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Create([FromBody] CreateSellerDTO sellerDTO)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            Seller seller = _mapper.Map<Seller>(sellerDTO);
            await _sellerRepository.CreateAsync(seller);

            return CreatedAtRoute("GetSellerById", new { seller.Id }, seller);
        }

        [Authorize(Roles = "Seller")]
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateSellerDTO sellerDTO)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            Seller seller = await _sellerRepository.FindByIdAsync(id);
            if (seller.IsNull()) return NotFound();

            _mapper.Map(sellerDTO, seller);
            await _sellerRepository.UpdateAsync(seller);

            return NoContent();
        }

        [Authorize(Roles = "Seller")]
        [HttpPatch]
        [Route("{id:int}")]
        public async Task<ActionResult> UpdatePartial([FromRoute] int id, [FromBody] JsonPatchDocument<UpdateSellerDTO> pacthSellerDTO)
        {
            Seller seller = await _sellerRepository.FindByIdAsync(id);
            if (seller.IsNull()) return NotFound();

            UpdateSellerDTO sellerDTO = _mapper.Map<UpdateSellerDTO>(seller);
            pacthSellerDTO.ApplyTo(sellerDTO);
            if (!TryValidateModel(sellerDTO)) return ValidationProblem(ModelState);

            _mapper.Map(sellerDTO, seller);
            await _sellerRepository.UpdateAsync(seller);

            return NoContent();
        }

        [Authorize(Roles = "Seller")]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            Seller seller = await _sellerRepository.FindByIdAsync(id);
            if (seller.IsNull()) return NotFound();

            await _sellerRepository.DeleteAsync(seller);

            return NoContent();
        }
    }
}
