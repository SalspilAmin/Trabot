using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Categorie.Commands.Models;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Features.ProductVariant.Commands.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers.Category
{
    [ApiController]
    public class SellerCategoryController : AppControllerBase
    {
        [HttpPost(Router.Category.Add)]
        public async Task<IActionResult> Add([FromForm] AddCategoryCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPut(Router.Category.Update)]
        public async Task<IActionResult> Update( [FromForm] UpdateCategoryCommand command)
        {
            

            var response = await Mediator.Send(command);
            return NewResult(response);

        }
        [HttpDelete(Router.Category.Delete)]

        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {

            var result = await Mediator.Send(new DeleteCategoryCommand(id));
            return NewResult(result);
        }


        [HttpPatch(Router.Category.restore)]
        public async Task<IActionResult> RestoreCategory([FromRoute] int id)
        {
            var result = await Mediator.Send(new RestoreCategoryCommand(id));
            return NewResult(result);
        }

    }
}
