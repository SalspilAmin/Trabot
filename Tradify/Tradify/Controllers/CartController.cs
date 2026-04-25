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

        [HttpGet(Router.Cart.GetByUserId)]
        public async Task<IActionResult> GetCart([FromRoute] int Id)
        {
            var result = await Mediator.Send(new GetCartByUserIdQuery(Id));
            return NewResult(result);
        }
        [HttpPut(Router.Cart.UpdateCart)]
        public async Task<IActionResult> Update([FromBody] UpdateCartCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }
    }
}
