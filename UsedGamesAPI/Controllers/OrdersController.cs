using AutoMapper;
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
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrdersController(IOrderRepository platformRepository, IMapper mapper)
        {
            _orderRepository = platformRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Platform>>> Get()
        {
            List<Order> orders = await _orderRepository.FindAllAsync();
            return Ok(orders);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetOrderById")]
        public async Task<ActionResult<Platform>> GetById([FromRoute] int id)
        {
            Order order = await _orderRepository.FindByIdAsync(id);
            if (order.IsNull()) return NotFound();

            return Ok(order);
        }
    }
}
