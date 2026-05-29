using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Comments.Commands.Models
{
    public class UpdateCommentCommand :
    IRequest<Response<string>>
    {
        public int CommentId { get; set; }

        public string Content { get; set; }
    }
}
