using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Comments.Commands.Models
{
    public class DeleteCommentCommand :
      IRequest<Response<string>>
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
    }
}
