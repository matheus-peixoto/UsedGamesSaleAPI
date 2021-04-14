using AutoMapper;
using ExthensionMethods.Object;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsedGamesAPI.DTOs.Order;
using UsedGamesAPI.Models;
using UsedGamesAPI.Repositories.Interfaces;

namespace UsedGamesAPI.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public OrdersController(IOrderRepository platformRepository, IClientRepository clientRepository, IGameRepository gameRepository, IMapper mapper)
        {
            _orderRepository = platformRepository;
            _clientRepository = clientRepository;
            _gameRepository = gameRepository;
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

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Create([FromBody] CreateOrderDTO orderDTO)
        {
            await ValidateOrderModel(orderDTO);
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            Order order = _mapper.Map<Order>(orderDTO);
            await _orderRepository.CreateAsync(order);

            return CreatedAtRoute("GetOrderById", new { order.Id }, order);
        }

        [NonAction]
        private async Task ValidateOrderModel(CreateOrderDTO orderDTO)
        {
            if (!await _clientRepository.ExistsAsync(orderDTO.ClientId))
                ModelState.AddModelError("ClientId", "The given client's id does not corresponds to an existing client");

            if (!await _gameRepository.ExistsAsync(orderDTO.GameId))
                ModelState.AddModelError("GameId", "The given game's id does not corresponds to an existing game");
        }
    }
}
