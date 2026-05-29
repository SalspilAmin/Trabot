using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Comments.Commands.Models
{
    public class AddCommentCommand : IRequest<Response<int?>>
    {
        public int PostId { get; set; }

        public int UserId { get; set; }

        public string Content { get; set; }
    }
}
