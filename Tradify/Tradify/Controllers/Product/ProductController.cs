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

        [HttpGet(Router.Product.BestSelling)]
        public async Task<IActionResult> GetProductsBestSelling([FromQuery] GetProductBestSellingQuery query)
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


        [HttpGet(Router.Product.Search)]
        public async Task<IActionResult> GetProductsListBySearch([FromQuery] GetProductBySearchListQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet(Router.Product.Store)]
        public async Task<IActionResult> GetProductByCategory([FromQuery] GetProductByStoreQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }


        [HttpGet(Router.Product.GetByID)]
        public async Task<IActionResult> GetById([FromRoute] int id )
        {
            var result = await Mediator.Send(new GetProductByIdQuery(id)); 
            return NewResult(result);
        }

        [HttpGet(Router.Product.Discount)]
        public async Task<IActionResult> GetProductsByDiscount([FromQuery] GetProductDiscountQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }


    }
}
