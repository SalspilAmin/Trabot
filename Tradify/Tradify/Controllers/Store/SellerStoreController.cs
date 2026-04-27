using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Features.Store.Commands.Models;
using Tradify.Core.Features.Store.Queries.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers.Store
{
   
    [ApiController]
    public class SellerStoreController : AppControllerBase
    {
        [HttpPut(Router.Store.UpdateStore)]
        public async Task<IActionResult> UpdateStore([FromForm] UpdateStoreCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

     

        [HttpGet(Router.Store.GetMyStore)]
        public async Task<IActionResult> GetMyStore()
        {
            var sellerId = int.Parse(User.FindFirst("SellerId")?.Value);

            var query = new GetMyStoreQuery(sellerId);

            var response = await Mediator.Send(query);
            return NewResult(response);
        }

        [HttpPut(Router.Store.ActivateStore)]
        public async Task<IActionResult> ActivateStore()
        {
            var sellerId = int.Parse(User.FindFirst("SellerId")?.Value);

            var command = new ActivateStoreCommand(sellerId);

            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPut(Router.Store.DeactivateStore)]
        public async Task<IActionResult> DeactivateStore()
        {
            var sellerId = int.Parse(User.FindFirst("SellerId")?.Value);

            var command = new DeactivateStoreCommand(sellerId);

            var response = await Mediator.Send(command);
            return NewResult(response);
        }
    }
}
