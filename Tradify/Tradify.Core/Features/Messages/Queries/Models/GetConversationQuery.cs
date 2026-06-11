using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Data.Helpers.Results;

namespace Tradify.Core.Features.Messages.Queries.Models
{
    public class GetConversationQuery :
     IRequest<Response<List<MessageResult>>>
    {
        public int UserOneId { get; set; }

        public int UserTwoId { get; set; }
    }
}
