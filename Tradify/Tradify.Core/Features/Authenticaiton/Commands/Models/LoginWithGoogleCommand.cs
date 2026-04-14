
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Data.Helpers;

namespace Tradify.Core.Features.Authenticaiton.Commands.Models
{
    public class LoginWithGoogleCommand : IRequest<Response<string>>
    {
        public string Code { get; set; }
    }
}
