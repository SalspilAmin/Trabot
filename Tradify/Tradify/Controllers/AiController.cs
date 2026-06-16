using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Ai.Queries.Models;
using Tradify.Core.Features.Recomndition.Queries.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers
{
    
    public class AiController : AppControllerBase
    {

        [HttpGet(Router.Ai.ProductRecommendation)]
        public async Task<IActionResult> GetProductRecommendationAi([FromQuery] AiQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
