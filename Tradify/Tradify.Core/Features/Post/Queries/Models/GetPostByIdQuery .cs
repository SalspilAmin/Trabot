using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Post.Queries.Results;

namespace Tradify.Core.Features.Post.Queries.Models
{
    public class GetPostByIdQuery : IRequest<Response<GetListOfPostsResult>>
    {
        public int PostId { get; set; }

        public GetPostByIdQuery(int postId)
        {
            PostId = postId;
        }
    }
}
