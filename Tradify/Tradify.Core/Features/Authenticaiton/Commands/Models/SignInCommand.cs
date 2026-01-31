using Tradify.Core.Bases;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Tradify.Core.Features.Authenticaiton.Commands.Models
{
    public class SignInCommand : IRequest< Response<string>>
    {
        public string  EmailOrPhone {get; set;}     

        public string Password {get; set;}
    }
}
