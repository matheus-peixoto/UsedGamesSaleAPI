using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;
using UsedGamesAPI.Models.DTOs.Image;
using UsedGamesAPI.Repositories.Interfaces;

namespace UsedGamesAPI.Services.Filters.ImageFilters
{
    public class ValidateImageForeignKeysOnUpdateAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            IGameRepository gameRepository = (IGameRepository)context.HttpContext.RequestServices.GetService(typeof(IGameRepository));
            UpdateImageDTO imgDTO = (UpdateImageDTO)context.ActionArguments["imageDTO"];

            if (imgDTO.GameId == 0 || !await gameRepository.ExistsAsync(imgDTO.GameId))
            {
                context.ModelState.AddModelError("GameId", "The given game id does not corresponds to an existing game");
            }

            await next();
        }
    }
}
