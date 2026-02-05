using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Authenticaiton.Commands.Models
{
    public class SendResetPasswordCommand : IRequest<Response<string>>
    {
       public  string EmailOrPhone { get; set; }      

    }
}
