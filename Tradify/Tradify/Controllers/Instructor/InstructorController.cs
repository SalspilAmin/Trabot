using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Instructor.Command.Models;
using Tradify.Core.Features.Instructor.Queries.Models;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Features.Product.Queries.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers.Instructor
{
   
    public class InstructorController : AppControllerBase
    {
        [HttpPost(Router.Instructor.Add)]
        public async Task<IActionResult> Add([FromForm] AddInstructorCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }


        [HttpPost(Router.Instructor.AddWithImage)]
        public async Task<IActionResult> AddWithImage([FromForm] AddInstructorWithImageCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpGet(Router.Instructor.JopTitle)]
        public async Task<IActionResult> GetInstructorJopTitlePagination([FromQuery] GetInstructorJopTitleQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet(Router.Instructor.GetByID)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetInstructorByIdQuery(id));
            return NewResult(result);
        }



        [HttpGet(Router.Instructor.Paginated)]
        public async Task<IActionResult> GetInstructorPagination([FromQuery] GetInstructorPagnitionQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }


        [HttpGet(Router.Instructor.WithDiscount)]
        public async Task<IActionResult> GetInstructorWithDiscount([FromQuery] GetInstructorWithDiscountQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
