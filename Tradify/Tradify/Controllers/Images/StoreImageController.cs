using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.ProductsImages.Commands.Models;
using Tradify.Core.Features.StoreImage.Commands.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers.Images
{
   
    public class StoreImageController : AppControllerBase
    {
        [HttpPost(Router.StoreImage.Add)]
        public async Task<IActionResult> AddImage([FromForm] AddStoreImageCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpPut(Router.StoreImage.Update)]
        public async Task<IActionResult> UpdateImage( [FromForm] UpdateStoreImageCommand command)
        {

            var result = await Mediator.Send(command);
            return NewResult(result);
        }



        [HttpDelete(Router.StoreImage.Delete)]
        public async Task<IActionResult> DeleteImage([FromRoute] int id)
        {
            var result = await Mediator.Send(new DeleteStoreImageCommand(id));
            return Ok(result);
        }

    }
}
