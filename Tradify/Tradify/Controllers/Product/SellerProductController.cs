using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tradify.Bases;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Features.Product.Queries.Models;
using Tradify.Core.Features.User.Commands.Models;
using Tradify.Core.Features.User.Queries.Models;
using Tradify.Data.AppMetaData;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Tradify.Controllers.Product
{
    
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
        public async Task<IActionResult> UpdateProduct([FromRoute] int productId, [FromForm] UpdateProductCommand command)
        {
            command.ProductId = productId;
            
            var response = await Mediator.Send(command);
            return NewResult(response);

        }
        //[HttpDelete(Router.Product.Delete)]
        //public async Task<IActionResult> DeleteProduct([FromQuery] DeleteProductCommand command)
        //{

        //    var response = await Mediator.Send(command);
        //    return Ok(response);
        //}
        [HttpDelete(Router.Product.Delete)]
        public async Task<IActionResult> DeleteProduct([FromRoute] int productId)
        {
            var response = await Mediator.Send(new DeleteProductCommand(productId));
            return Ok(response);
        }
        [HttpPut(Router.Product.restore)]
        public async Task<IActionResult> RestoreProduct([FromRoute] int productId)
        {

            var response = await Mediator.Send(new RestoreProductCommand(productId));
            return Ok(response);
        }


        [HttpGet(Router.Product.MyProducts)]
        public async Task<IActionResult> GetMyProducts([FromQuery] GetSellerProductsQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result); 
        }



    }
}
