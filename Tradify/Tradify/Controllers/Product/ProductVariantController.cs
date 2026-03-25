using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Features.ProductVariant.Commands.Models;
using Tradify.Core.Features.ProductVariant.Queries.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers.Product
{
    [ApiController]
    public class ProductVariantController : AppControllerBase
    {

        [HttpPost(Router.ProductVariant.Add)]
        public async Task<IActionResult> AddVariant([FromRoute] int productId, [FromBody] AddProductVariantCommand command)
        {
            command.ProductId = productId;
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpPut(Router.ProductVariant.UpdateProductVariant)]
        public async Task<IActionResult> UpdateProductVariant([FromRoute] int variantId, [FromBody] UpdateProductVariantCommand command)
        {
            command.Id = variantId;
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

        [HttpPut(Router.ProductVariant.AddDiscount)]
        public async Task<IActionResult> AddDiscount([FromBody] AddDiscountCommand command)
        {
 
            var response = await Mediator.Send(command);
            return NewResult(response);

        }
        [HttpDelete(Router.ProductVariant.DeleteDiscount)]
        public async Task<IActionResult> DeleteDiscount([FromQuery] DeleteDiscountCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpGet(Router.ProductVariant.GetByProduct)]
        public async Task<IActionResult> GetVariantsByProduct([FromQuery] GetProductVariantsByProductQuery request)
        {
            var result = await Mediator.Send(request);
           return  Ok(result);
        }

        [HttpGet(Router.ProductVariant.GetById)]
        public async Task<IActionResult> GetVariantById([FromRoute] int variantId)
        {
            var result = await Mediator.Send(new GetProductVariantByIdQuery(variantId));
            return NewResult(result);
        }

    }
        }
    
