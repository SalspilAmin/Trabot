using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Features.Product.Queries.Models;
using Tradify.Core.Features.User.Commands.Models;
using Tradify.Core.Features.User.Queries.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers
{
    [Route("api/[controller]")]
   
    public class ProductController : AppControllerBase
    {
        [HttpPost(Router.Product.Add)]
        public async Task<IActionResult> Add([FromForm] AddProductCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpGet(Router.Product.Paginated)]
        public async Task<IActionResult> GetProductsPagination([FromQuery] GetProductPaginationQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet(Router.Product.GetByID)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetProductByIdQuery(id));
            return NewResult(result);
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
