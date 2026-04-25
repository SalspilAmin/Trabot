using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Features.ProductVariant.Commands.Models;
using Tradify.Core.Features.ProductVariant.Queries.Models;
using Tradify.Core.Features.Store.Queries.Models;
using Tradify.Core.Features.Discount.Varint.Comands.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers.Product
{
    [ApiController]
    public class ProductVariantController : AppControllerBase
    {

        [HttpPost(Router.ProductVariant.Add)]
        public async Task<IActionResult> AddVariant( [FromForm] AddProductVariantCommand command)
        {
            
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpPost(Router.ProductVariant.AddWithImage)]
        public async Task<IActionResult> AddVariantWithImage([FromForm] AddProductVarintWithImageCommand command)
        {

            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpPut(Router.ProductVariant.UpdateProductVariant)]
        public async Task<IActionResult> UpdateProductVariant( [FromForm] UpdateProductVariantCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }
       

        [HttpDelete(Router.ProductVariant.Delete)]

        public async Task<IActionResult> DeleteVariant([FromRoute] int id)
        {

            var result = await Mediator.Send(new DeleteProductVariantCommand(id));
            return NewResult(result);
        }

        [HttpPatch(Router.ProductVariant.restore)]
        public async Task<IActionResult> RestoreVariant([FromRoute] int id)
        {
            var result = await Mediator.Send(new RestoreProductVariantCommand(id));
            return NewResult(result);
        }

      
        [HttpGet(Router.ProductVariant.GetByProduct)]
        public async Task<IActionResult> GetVariantsByProduct([FromQuery] GetProductVariantsByProductQuery request)
        {
            var result = await Mediator.Send(request);
           return  Ok(result);
        }

        [HttpGet(Router.ProductVariant.GetById)]
        public async Task<IActionResult> GetVariantById([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetProductVariantByIdQuery(id));
            return NewResult(result);
        }


        [HttpGet(Router.ProductVariant.List)]
        public async Task<IActionResult> GetAllVarintList([FromQuery] GetAllVarintByProductListQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }




    }
 }
    
