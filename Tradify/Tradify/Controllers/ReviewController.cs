using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Review.Queries.Results;
using Tradify.Core.Features.Review.Commands.Models;
using Tradify.Core.Features.Review.Queries.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers
{
    [ApiController]
    public class ReviewController : AppControllerBase
    {
        [HttpPost(Router.Review.AddProductReview)]
        public async Task<IActionResult> AddProductReview([FromForm] AddProductReviewCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpPost(Router.Review.AddInstructorReview)]
        public async Task<IActionResult> AddInstructorReview([FromForm] AddInstructorReviewCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPut(Router.Review.Update)]
        public async Task<IActionResult> UpdateReview([FromForm] UpdateReviewCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);

        }
        [HttpDelete(Router.Review.Delete)]
        public async Task<IActionResult> DeleteReview([FromRoute] int id)
        {
            var response = await Mediator.Send(new DeleteReviewCommand(id));
            return Ok(response);
        }

        [HttpGet(Router.Review.ProductReviews)]
        public async Task<IActionResult> GetProductReviewsPagination([FromQuery] GetProductReviewsQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }



        [HttpGet(Router.Review.InstructorReviews)]
        public async Task<IActionResult> GetInstructorReviewsPagination([FromQuery] GetInstructorReviewQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
