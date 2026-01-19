using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.User.Commands.Models
{
    public class AddUserCommand : IRequest<Response<string>>
    {
        public string EmailOrPhone { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
