using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Features.ProductsImages.Commands.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers.Images
{
    public class ProductImageController : AppControllerBase
    {
     

        [HttpPost(Router.ProductImage.Add)]
        public async Task<IActionResult> AddImage([FromForm] AddProductImageCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpPut(Router.ProductImage.Update)]
        public async Task<IActionResult> UpdateImage([FromRoute] int imageId, [FromForm] UpdateProductImageCommand command)
        {
           
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpDelete(Router.ProductImage.Delete)]
        public async Task<IActionResult> DeleteImage([FromRoute] int imageId)
        {
            var command = new DeleteProductImageCommand(imageId);
            var result = await Mediator.Send(command);
            return NewResult(result);
        }


    }
}
