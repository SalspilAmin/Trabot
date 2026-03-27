

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Authenticaiton.Commands.Models;
using Tradify.Core.Features.Authenticaiton.Queries.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers
{
   
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        [HttpPost(Router.Authentication.LogIN)]
        public async Task<IActionResult> LogIn([FromForm] SignInCommand request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);

        }
        [HttpPut(Router.Authentication.RefreshToken)]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommand request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);
        }
        [HttpGet(Router.Authentication.ConfirmEmail)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] int userId, [FromQuery] string code )
        {
            var result = await Mediator.Send(new ConfirmEmailQuery() {Id=userId,Code=code });
            return NewResult(result);
        }
        [HttpPost(Router.Authentication.ConfrimPhone)]
        public async Task<IActionResult> ConfrimPhone([FromBody] ConfirmPhoneQuery request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);

        }
        [HttpPost(Router.Authentication.SendResetPassword)]
        public async Task<IActionResult> SendResetPasswordCode([FromQuery]SendResetPasswordCommand request)
        {
            var result = await Mediator.Send(request);
                return NewResult(result);   
        }
        [HttpPost(Router.Authentication.ConfirmResetPassword)]
        public async Task<IActionResult> ConfirmResetPassword([FromBody] ConfirmResetPasswordCommand request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);   
        }
        [HttpPost(Router.Authentication.ResetPassword)]
       public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);
        }

        [HttpGet(Router.Authentication.LoginGoogle)]
        public async Task<IActionResult> LoginGoogle()
        {
            var result = await Mediator.Send(new BeginCoonectionWithGoogleCommand());
            return NewResult(result);
        }


        [HttpGet(Router.Authentication.LoginGoogleCallBack)]
        public  async Task<IActionResult> GoogleCallBack([FromQuery] string code)
        {
            var result = await Mediator.Send(new LoginWithGoogleCommand() { Code= code});
            return  NewResult(result);
        }
    }
}
