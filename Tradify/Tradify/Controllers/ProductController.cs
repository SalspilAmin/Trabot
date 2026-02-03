using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Product.Commands.Models;
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
    }
}
