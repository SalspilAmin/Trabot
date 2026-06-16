using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Instructor.Queries.Models;
using Tradify.Core.Features.Recomndition.Queries.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers
{
   
    public class RecommendationController : AppControllerBase
    {

        [HttpGet(Router.Recommendation.Instructors)]
        public async Task<IActionResult> GetRecommendedInstructors([FromQuery] GetRecommendedInstructorsQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet(Router.Recommendation.TopRatedProduct)]
        public async Task<IActionResult> GetRecommendedTopRatedProduct([FromQuery] GetTopRatedProductsRecommendQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }


        [HttpGet(Router.Recommendation.TopRatedInstructor)]
        public async Task<IActionResult> GetRecommendedTopRatedInstructor([FromQuery] GetTopRatedInstructorsQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
