using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Comments.Commands.Models
{
    public class UpdateReplyCommentCommand :
     IRequest<Response<string>>
    {
        public int ReplyId { get; set; }

        public string Content { get; set; }
    }
}
