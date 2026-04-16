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

    [ApiController]

    public class ProductController : AppControllerBase
    {


        [HttpGet(Router.Product.Paginated)]
        public async Task<IActionResult> GetProductsPagination([FromQuery] GetProductPaginationQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
        [HttpGet(Router.Product.List)]
        public async Task<IActionResult> GetProductsList([FromQuery] GetAllProductListQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        

        [HttpGet(Router.Product.GetByID)]
        public async Task<IActionResult> GetById([FromRoute] int id )//,int userId)
        {
            var result = await Mediator.Send(new GetProductByIdQuery(id));  //, userId));
            return NewResult(result);
        }

        //[HttpGet(Router.Product.Category)]
        //public async Task<IActionResult> GetProductByCategory([FromQuery] GetProductsByCategoryQuery query)
        //{
        //    if (query == null || query.CategoryId <= 0)
        //        return BadRequest("CategoryId is required and must be greater than 0");

        //    var result = await Mediator.Send(query);
        //    return Ok(result);
        //}


    }
}
