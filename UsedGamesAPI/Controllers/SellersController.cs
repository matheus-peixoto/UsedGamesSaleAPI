﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsedGamesAPI.Models;
using UsedGamesAPI.Repository.Interfaces;

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
        public async Task<ActionResult<List<Seller>>> Get()
        {
            List<Seller> sellers = await _sellerRespository.FindAllAsync();
            return Ok(sellers);
        }


    }
}