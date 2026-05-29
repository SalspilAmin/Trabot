using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Comments.Commands.Models
{
    public class AddReplyCommentCommand :
    IRequest<Response<int?>>
    {
        public int CommentId { get; set; }

        public int UserIdThatWriteAComment { get; set; }

        public int UserIdThatWriteAReplyOFComment { get; set; }

        public string Content { get; set; }
    }
}
