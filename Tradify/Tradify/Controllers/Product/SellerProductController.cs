using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Features.Product.Queries.Models;
using Tradify.Core.Features.User.Commands.Models;
using Tradify.Core.Features.User.Queries.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerProductController : AppControllerBase
    {
        [HttpPost(Router.Product.Add)]
        public async Task<IActionResult> Add([FromForm] AddProductCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPut(Router.Product.UpdateProduct)]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);

        }
        [HttpDelete(Router.Product.Delete)]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            var response = await Mediator.Send(new DeleteProductCommand(id));
            return Ok(response);
        }
    }
}
