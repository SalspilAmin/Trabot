using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Certification.Command.Models;
using Tradify.Core.Features.Certification.Queries.Models;
using Tradify.Core.Features.Education.Command.Models;
using Tradify.Core.Features.Education.Queries.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers.Instructor
{
    
    public class CertificationsController : AppControllerBase
    {
        [HttpPost(Router.InstructorCertification.Add)]
        public async Task<IActionResult> Add([FromForm] AddInstructorCertificationCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpGet(Router.InstructorCertification.GetByInstructor)]
        public async Task<IActionResult> GetInstructorCertification([FromQuery] int id)
        {
            var result = await Mediator.Send(new GetCertificationByInstructorQuery(id));
            return Ok(result);
        }

        [HttpPut(Router.InstructorCertification.UpdateCertification)]
        public async Task<IActionResult> UpdateCertification([FromForm] UpdateInstructorCertificationCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete(Router.InstructorCertification.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var resutl = await Mediator.Send(new DeleteInstructorCertificationCommand(id));
            return Ok(resutl);
        }
    }
}
