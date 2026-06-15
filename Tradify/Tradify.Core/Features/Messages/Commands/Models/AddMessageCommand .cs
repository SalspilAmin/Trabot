using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Messages.Commands.Models
{
    public class AddMessageCommand :
     IRequest<Response<int?>>
    {
        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public string? Content { get; set; }

        public List<IFormFile>? MediaFiles { get; set; }
    }
}
