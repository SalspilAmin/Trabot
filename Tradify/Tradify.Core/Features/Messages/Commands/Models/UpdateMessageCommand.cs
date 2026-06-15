using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Messages.Commands.Models
{
    public class UpdateMessageCommand :
    IRequest<Response<string>>
    {
        public int MessageId { get; set; }

        public string Content { get; set; }
    }
}
