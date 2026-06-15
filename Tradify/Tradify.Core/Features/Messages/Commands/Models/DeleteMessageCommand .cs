using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Messages.Commands.Models
{
    public class DeleteMessageCommand :
     IRequest<Response<string>>
    {
        public int MessageId { get; set; }
    }
}
