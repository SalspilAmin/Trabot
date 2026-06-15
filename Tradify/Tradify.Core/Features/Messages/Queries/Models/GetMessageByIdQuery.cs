using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Data.Helpers.Results;

namespace Tradify.Core.Features.Messages.Queries.Models
{
    public class GetMessageByIdQuery :
      IRequest<Response<MessageResult>>
    {
        public int MessageId { get; set; }
    }
}
