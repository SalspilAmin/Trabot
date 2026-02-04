using MediatR;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Authenticaiton.Queries.Models
{
    public  class ConfirmPhoneQuery : IRequest<Response<string>>
    {
        public int Id { get; set; }

        public string OTP { get; set; } 
    }
}
