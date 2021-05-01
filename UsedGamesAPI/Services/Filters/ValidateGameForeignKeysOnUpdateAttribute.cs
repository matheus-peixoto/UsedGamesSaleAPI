using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsedGamesAPI.DTOs.Games;
using UsedGamesAPI.Repositories.Interfaces;

namespace UsedGamesAPI.Services.Filters
{
    public class ValidateGameForeignKeysOnUpdateAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            IPlatformRepository platformRepository = (IPlatformRepository)context.HttpContext.RequestServices.GetService(typeof(IPlatformRepository));
            ISellerRepository sellerRepository = (ISellerRepository)context.HttpContext.RequestServices.GetService(typeof(ISellerRepository));
            UpdateGameDTO gameDTO = (UpdateGameDTO)context.ActionArguments["gameDTO"];

            if (!await platformRepository.ExistsAsync(gameDTO.PlatformId))
            {
                context.ModelState.AddModelError("PlatformId", "The given platform id does not correspond to an existing Platform");
            }

            if (!await sellerRepository.ExistsAsync(gameDTO.SellerId))
            {
                context.ModelState.AddModelError("SellerId", "The given seller id does not correspond to an existing seller");
            }
            
            await next();
        }
    }
}
