using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.ProductsImages.Commands.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers.Images
{
    [ApiController]
    public class ProductVariantImageController : AppControllerBase
    {
        [HttpPost(Router.ProductVariantImage.Add)]
        public async Task<IActionResult> AddImage([FromForm] AddProductVariantImageCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpPut(Router.ProductVariantImage.Update)]
        public async Task<IActionResult> UpdateImage([FromRoute] int imageId, [FromForm] UpdateProductVariantImageCommand command)
        {

            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpDelete(Router.ProductVariantImage.Delete)]
        public async Task<IActionResult> DeleteImage([FromRoute] int imageId)
        {
            var command = new DeleteProductVariantImageCommand(imageId);
            var result = await Mediator.Send(command);
            return NewResult(result);
        }
    }
}
