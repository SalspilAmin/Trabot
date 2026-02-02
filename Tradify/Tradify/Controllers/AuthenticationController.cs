

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Authenticaiton.Commands.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        [HttpPost(Router.Authentication.LogIN)]
        public async Task<IActionResult> LogIn([FromForm] SignInCommand request)
        {
            var result =await Mediator.Send(request);
            return NewResult(result);

        }
        [HttpPut(Router.Authentication.RefreshToken)]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommand request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);       
        }
    }
}
