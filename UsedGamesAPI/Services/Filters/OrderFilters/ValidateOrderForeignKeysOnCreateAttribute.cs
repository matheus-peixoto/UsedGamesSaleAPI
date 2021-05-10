using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;
using UsedGamesAPI.DTOs.Order;
using UsedGamesAPI.Repositories.Interfaces;

namespace UsedGamesAPI.Services.Filters.OrderFilters
{
    public class ValidateOrderForeignKeysOnCreateAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            IGameRepository gameRepository = (IGameRepository)context.HttpContext.RequestServices.GetService(typeof(IGameRepository));
            IClientRepository clientRepository = (IClientRepository)context.HttpContext.RequestServices.GetService(typeof(IClientRepository));
            CreateOrderDTO orderDTO = (CreateOrderDTO)context.ActionArguments["orderDTO"];

            if (!await clientRepository.ExistsAsync(orderDTO.ClientId))
                context.ModelState.AddModelError("ClientId", "The given client's id does not corresponds to an existing client");

            if (!await gameRepository.ExistsAsync(orderDTO.GameId))
                context.ModelState.AddModelError("GameId", "The given game's id does not corresponds to an existing game");

            await next();
        }
    }
}
