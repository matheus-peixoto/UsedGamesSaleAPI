using AutoMapper;
using ExthensionMethods.Object;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsedGamesAPI.DTOs.Order;
using UsedGamesAPI.Models;
using UsedGamesAPI.Repositories.Interfaces;
using UsedGamesAPI.Services.Filters.OrderFilters;

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
        [ValidateOrderForeignKeysOnCreate]
        public async Task<ActionResult> Create([FromBody] CreateOrderDTO orderDTO)
        {
            Order order = _mapper.Map<Order>(orderDTO);
            await _orderRepository.CreateAsync(order);

            return CreatedAtRoute("GetOrderById", new { order.Id }, order);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateOrderDTO orderDTO)
        {
            Order order = await _orderRepository.FindByIdAsync(id);

            if (order.IsNull()) return NotFound();

            order = _mapper.Map(orderDTO, order);
            await _orderRepository.UpdateAsync(order);

            return NoContent();
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<ActionResult> UpdatePartial([FromRoute] int id, [FromBody] JsonPatchDocument<UpdateOrderDTO> patchOrderDTO)
        {
            Order order = await _orderRepository.FindByIdAsync(id);
            if (order.IsNull()) return NotFound();

            UpdateOrderDTO orderDTO = _mapper.Map<UpdateOrderDTO>(order);
            patchOrderDTO.ApplyTo(orderDTO);
            if (!TryValidateModel(orderDTO)) return ValidationProblem(ModelState);

            _mapper.Map(orderDTO, order);
            await _orderRepository.UpdateAsync(order);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            Order order = await _orderRepository.FindByIdAsync(id);
            if (order.IsNull()) return NotFound();

            await _orderRepository.DeleteAsync(order);

            return Ok(order);
        }
    }
}
