
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.User.Commands.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers
{
    [Route("api/[controller]")]
    public class UserController : AppControllerBase
    {

        public UserController(IMediator mediator) : base(mediator) { }
        [HttpPost(Router.UserRouter.Create)]

        public async Task<IActionResult> Create([FromBody] AddUserCommand command)
        {
         var response= await  Mediator.Send(command);
            return NewResult(response);
        }

    }
}
