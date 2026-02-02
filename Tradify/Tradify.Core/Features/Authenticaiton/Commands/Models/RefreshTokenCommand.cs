using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Data.Helpers;

namespace Tradify.Core.Features.Authenticaiton.Commands.Models
{
    public class RefreshTokenCommand : IRequest<Response<JwtAuthResult>>
    {
        public string Accesstoken { get; set; }

        public string RefreshToken { get; set; }
    }
}
