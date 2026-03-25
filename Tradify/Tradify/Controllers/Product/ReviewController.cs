using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Review.Queries.Results;
using Tradify.Core.Features.Review.Commands.Models;
using Tradify.Core.Features.Review.Queries.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers.Review
{
    [ApiController]
    public class ReviewController : AppControllerBase
    {
        [HttpPost(Router.Review.Add)]
        public async Task<IActionResult> Add([FromForm] AddReviewCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpPut(Router.Review.Update)]
        public async Task<IActionResult> UpdateReview([FromRoute] int RwviewId, [FromForm] UpdateReviewCommand command)
        {
            command.Id = RwviewId;

            var response = await Mediator.Send(command);
            return NewResult(response);

        }
        [HttpDelete(Router.Review.Delete)]
        public async Task<IActionResult> DeleteReview([FromRoute] int RwviewId)
        {
            var response = await Mediator.Send(new DeleteReviewCommand(RwviewId));
            return Ok(response);
        }

        [HttpGet(Router.Review.Paginated)]
        public async Task<IActionResult> GetProductReviewsPagination([FromQuery] GetProductReviewsQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

     
    }
}
