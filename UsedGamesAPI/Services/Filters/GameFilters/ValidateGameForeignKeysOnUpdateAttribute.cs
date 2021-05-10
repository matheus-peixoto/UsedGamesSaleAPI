using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;
using UsedGamesAPI.DTOs.Games;
using UsedGamesAPI.Repositories.Interfaces;

namespace UsedGamesAPI.Services.Filters.GameFilters
{
    public class ValidateGameForeignKeysOnUpdateAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            IPlatformRepository platformRepository = (IPlatformRepository)context.HttpContext.RequestServices.GetService(typeof(IPlatformRepository));
            UpdateGameDTO gameDTO = (UpdateGameDTO)context.ActionArguments["gameDTO"];

            if (gameDTO.PlatformId == 0 || !await platformRepository.ExistsAsync(gameDTO.PlatformId))
            {
                context.ModelState.AddModelError("PlatformId", "The given platform id does not correspond to an existing Platform");
            }

            await next();
        }
    }
}
