using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Certification.Command.Models;
using Tradify.Core.Features.Education.Command.Models;
using Tradify.Core.Features.Education.Queries.Models;
using Tradify.Core.Features.InstructorService.Command.Models;
using Tradify.Core.Features.InstructorService.Queries.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers.Instructor
{
    
    public class InstructorServiceController : AppControllerBase
    {
        [HttpPost(Router.InstructorService.Add)]
        public async Task<IActionResult> Add([FromForm] AddInstructorServiceCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpGet(Router.InstructorService.GetByInstructor)]
        public async Task<IActionResult> GetInstructorService([FromQuery] int id)
        {
            var result = await Mediator.Send(new GetInstructorServiceQuery(id));
            return Ok(result);
        }


        [HttpPut(Router.InstructorService.UpdateService)]
        public async Task<IActionResult> UpdateService([FromForm] UpdateInstructorServiceCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete(Router.InstructorService.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var resutl = await Mediator.Send(new DeleteInstructorServiceCommand(id));
            return Ok(resutl);
        }
    }
}
