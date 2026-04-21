using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Categorie.Queries.Models;
using Tradify.Core.Features.Product.Queries.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers.Category
{
    [ApiController]
    public class CategoryController :  AppControllerBase
    {
       

        [HttpGet(Router.Category.GetAll)]
        public async Task<IActionResult> GetAllCategory([FromQuery] GetAllCategoriesQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }


        [HttpGet(Router.Category.Tree)]
        public async Task<IActionResult> GetCategoryTree([FromQuery] GetCategoryTreeQuery query)
        {
            var result = await Mediator.Send(query);
            return NewResult(result);
        }

        [HttpGet(Router.Category.GetByID)]
        public async Task<IActionResult> GetCategoryById([FromRoute]int id,[FromQuery] bool includeChildren = false)
        {

            var result = await Mediator.Send(new GetCategoryByIdQuery
            {
                Id = id,
                IncludeChildren = includeChildren
            });
            return NewResult(result);
        }
    }
}
  