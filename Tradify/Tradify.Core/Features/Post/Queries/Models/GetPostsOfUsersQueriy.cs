using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Post.Queries.Results;
using Tradify.Data.Entities.Posts;

namespace Tradify.Core.Features.Post.Queries.Models
{
    public  class GetPostsOfUsersQueriy : IRequest<Response<IList<GetListOfPostsResult>>>
    {
        public int UserId { get; set; }
        public GetPostsOfUsersQueriy(int userId)
        {
            UserId = userId;
        }
    }
}
