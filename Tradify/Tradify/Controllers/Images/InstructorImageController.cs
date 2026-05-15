using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.InstructorImage.Command.Models;
using Tradify.Core.Features.StoreImage.Commands.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers.Images
{
    public class InstructorImageController : AppControllerBase
    {
        [HttpPost(Router.InstructorImage.Add)]
        public async Task<IActionResult> AddImage([FromForm] AddInstructorImageCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpPut(Router.InstructorImage.Update)]
        public async Task<IActionResult> UpdateImage([FromForm] UpdateInstructorImageCommand command)
        {

            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpDelete(Router.InstructorImage.Delete)]
        public async Task<IActionResult> DeleteImage([FromRoute] int id)
        {
            var result = await Mediator.Send(new DeleteInstructorImageCommand(id));
            return Ok(result);
        }

    }
}
