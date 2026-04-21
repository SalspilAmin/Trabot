
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.User.Commands.Models;
using Tradify.Core.Features.User.Queries.Models;
using Tradify.Core.Features.User.Queries.Results;
using Tradify.Core.Wrappers;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers
{

    public class UserController : AppControllerBase
    {

        //public UserController(IMediator mediator) : base(mediator) { }
        [HttpPost(Router.UserRouter.Create)]

        public async Task<IActionResult> Create([FromForm] AddUserCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }


        [HttpPut(Router.UserRouter.ChangePassword)]
        public async Task<IActionResult> ChangePasswrod([FromForm] ChangeUserPasswordCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);

        }
        [HttpGet(Router.UserRouter.GetByID)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetUserByIdQuery(id));
            return NewResult(result);
        }
        [HttpGet(Router.UserRouter.GetUserByToken)]
        public async Task<IActionResult> GetUserByToken([FromQuery] string token)
        {
            var result = await Mediator.Send(new GetUserByTokenQuery(token));
            return NewResult(result);
        }
        [HttpGet(Router.UserRouter.Paginated)]
        public async Task<IActionResult> GetPagination([FromQuery] GetUserPaginationQuery request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }
        [HttpDelete(Router.UserRouter.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var resutl = await Mediator.Send(new DeleteUserCommand(id));
            return Ok(resutl);
        }


    }
}
