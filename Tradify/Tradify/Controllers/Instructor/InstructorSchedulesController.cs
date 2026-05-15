using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Certification.Command.Models;
using Tradify.Core.Features.Education.Command.Models;
using Tradify.Core.Features.Education.Queries.Models;
using Tradify.Core.Features.InstructorSchedules.Command.Models;
using Tradify.Core.Features.InstructorSchedules.Queries.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers.Instructor
{
  
    public class InstructorSchedulesController : AppControllerBase
    {
        [HttpPost(Router.InstructorSchedules.Add)]
        public async Task<IActionResult> Add([FromForm] AddInstructorSchedulesCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpGet(Router.InstructorSchedules.GetByInstructor)]
        public async Task<IActionResult> GetInstructorSchedules([FromQuery] int id)
        {
            var result = await Mediator.Send(new GetInstructorSchedulesQuery(id));
            return Ok(result);
        }

        [HttpGet(Router.InstructorSchedules.GetNotAvilable)]
        public async Task<IActionResult> GetInstructorSchedulesNotAvilable([FromForm] GetNotAvilableInstructoreSchedulesQuery Query)
        {
            var result = await Mediator.Send(Query);
            return Ok(result);
        }

        [HttpPut(Router.InstructorSchedules.UpdateSchedules)]
        public async Task<IActionResult> UpdateSchedules([FromForm] UpdateInstructorSchedulesCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete(Router.InstructorSchedules.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var resutl = await Mediator.Send(new DeleteInstructorSchedulesCommand(id));
            return Ok(resutl);
        }

        [HttpPut(Router.InstructorSchedules.Restore)]
        public async Task<IActionResult> Restore([FromRoute] int id)
        {
            var resutl = await Mediator.Send(new RestoreInstructorSchedulesCommand(id));
            return Ok(resutl);
        }
    }
}
