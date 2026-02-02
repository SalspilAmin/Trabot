using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Authenticaiton.Queries.Models
{
    public class ConfirmEmailQuery : IRequest<Response<string>>
    {
        public int  Id { get; set; }

        public string Code { get; set; }

    }
}
