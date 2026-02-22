using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Authorization.Commands.Models;
using Tradify.Core.Features.Authorization.Queries.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers
{
 
    [ApiController]
    public class AuthorizationController : AppControllerBase
    {
        [HttpGet(Router.Authorization.GitList)]
        public async Task<IActionResult> GEtList()
        {
            var result = await Mediator.Send(new GetRolesListQuery());

            return NewResult(result);
        }
        [HttpGet(Router.Authorization.GitByID)]
        public async Task<IActionResult> GetByID([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetRoleByIdQuery() { Id = id });
            return NewResult(result);
        }
       

        [HttpPost(Router.Authorization.Create)]
        public async Task<IActionResult> Create([FromForm] AddRoleCommand request)
        {
            var result = await Mediator.Send(request);

            return NewResult(result);
        }

        [HttpPut(Router.Authorization.Edit)]
        public async Task<IActionResult> Edit([FromForm] EditRoleCommand request)
        {
            var result = await Mediator.Send(request);

            return NewResult(result);
        }

        [HttpDelete(Router.Authorization.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await Mediator.Send(new DeleteRoleCommand(id))
                ;
            return NewResult(result);
        }
        [HttpGet(Router.Authorization.ManageUserRolesList)]
        public async Task<IActionResult> GetUserRolesListToManage([FromRoute] int id)
        {
            var result = await Mediator.Send(new ManageUserRolesQuery(id));
            return NewResult(result);

        }
        [HttpPut(Router.Authorization.UpdateUserRoles)]
        public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesCommand request)
        {
            var result = await Mediator.Send(request);

            return NewResult(result);
        }
        [HttpGet(Router.Authorization.ManageUserClaims)]
        public async Task<IActionResult> ManageUserClaims([FromRoute] int userId)
        {
            var result = await Mediator.Send(new ManageUserClaimsQuery() { UserId = userId });
            return NewResult(result);
        }
        [HttpPut(Router.Authorization.UpdateUserClaims)]
        public async Task<IActionResult> UpdateUserClaims([FromBody] UpdateUserClaimsCommand request)
        {
            var result = await Mediator.Send(request);

            return NewResult(result);
        }
    }
}
