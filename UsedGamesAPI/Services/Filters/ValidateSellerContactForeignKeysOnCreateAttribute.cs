using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;
using UsedGamesAPI.DTOs.SellerContacts;
using UsedGamesAPI.Repositories.Interfaces;

namespace UsedGamesAPI.Services.Filters
{
    public class ValidateSellerContactForeignKeysOnCreateAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ISellerRepository sellerRepository = (ISellerRepository)context.HttpContext.RequestServices.GetService(typeof(ISellerRepository));
            CreateSellerContactDTO sellerContactDTO = (CreateSellerContactDTO)context.ActionArguments["sellerContactDTO"];

            if (!await sellerRepository.ExistsAsync(sellerContactDTO.SellerId))
                context.ModelState.AddModelError("SellerId", "The given seller id does not correspond to an existing seller");

            await next();
        }
    }
}
