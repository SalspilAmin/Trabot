using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Data.Helpers.Results;

namespace Tradify.Core.Features.Comments.Queries.Models
{
    public class GetCommentsByPostIdQuery :
         IRequest<Response<List<CommentResult>>>
    {
        public int PostId { get; set; }
    }
}
