using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Education.Command.Models;
using Tradify.Core.Features.Education.Queries.Models;
using Tradify.Core.Features.Instructor.Command.Models;
using Tradify.Core.Features.ProductVariant.Queries.Models;
using Tradify.Core.Features.Store.Commands.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers.Instructor
{
   
    public class EducationController : AppControllerBase
    {
        [HttpPost(Router.InstructorEducation.Add)]
        public async Task<IActionResult> Add([FromForm] AddInstructorEducationCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpGet(Router.InstructorEducation.GetByInstructor)]
        public async Task<IActionResult> GetInstructorEducation([FromQuery] int  id)
        {
            var result = await Mediator.Send(new GetEducationByInstructorQuery(id));
            return Ok(result);
        }

        [HttpPut(Router.InstructorEducation.UpdateEducation)]
        public async Task<IActionResult> UpdateEducation([FromForm] UpdateInstructorEducationCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete(Router.InstructorEducation.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var resutl = await Mediator.Send(new DeleteInstructorEducationCommand(id));
            return Ok(resutl);
        }
    }
}
