using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;
using UsedGamesAPI.DTOs.ClientContacts;
using UsedGamesAPI.Repositories.Interfaces;

namespace UsedGamesAPI.Services.Filters
{
    public class ValidateClientContactForeignKeysOnCreateAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            IClientRepository clientRepository = (IClientRepository)context.HttpContext.RequestServices.GetService(typeof(IClientRepository));
            CreateClientContactDTO clientContactDTO = (CreateClientContactDTO)context.ActionArguments["clientContactDTO"];

            if (!await clientRepository.ExistsAsync(clientContactDTO.ClientId))
                context.ModelState.AddModelError("ClientId", "The given client id does not corresponds to an existing client");

            await next();
        }
    }
}
