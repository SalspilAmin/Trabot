using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Cart.Commands.Models;
using Tradify.Core.Features.Cart.Queries.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers
{
    
    [ApiController]
    public class CartController : AppControllerBase
    {

        [HttpGet(Router.Cart.GetByToken)]
        public async Task<IActionResult> GetCart([FromRoute] string Token)
        {
            var result = await Mediator.Send(new GetCartByTokenQuery(Token));
            return NewResult(result);
        }
        [HttpPut(Router.Cart.UpdateCart)]
        public async Task<IActionResult> Update([FromBody] UpdateCartCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }
        [HttpPost(Router.Cart.AddToCart)]
        public async Task<IActionResult> AddToCArt([FromQuery] AddToCartCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }
    }
}
