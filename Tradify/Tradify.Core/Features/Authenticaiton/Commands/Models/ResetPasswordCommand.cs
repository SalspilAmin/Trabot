using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Authenticaiton.Commands.Models
{
    public class ResetPasswordCommand : IRequest<Response<string>>
    {
        public string EmailOrPhone { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
