using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Data.Helpers.Results;

namespace Tradify.Core.Features.Comments.Queries.Models
{
    public class GetReplyByIdQuery :
    IRequest<Response<ReplyCommentResult>>
    {
        public int ReplyId { get; set; }
    }
}
