using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Data.Helpers.Results;

namespace Tradify.Core.Features.Comments.Queries.Models
{
    public class GetRepliesByCommentIdQuery :
     IRequest<Response<List<ReplyCommentResult>>>
    {
        public int CommentId { get; set; }
    }
}
