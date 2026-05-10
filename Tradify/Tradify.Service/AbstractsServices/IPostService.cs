using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Posts;
using Tradify.Infrastructure.AbstractsRepositories;

namespace Tradify.Service.AbstractsServices
{
    public interface IPostService : IPostRepository
    {
        public Task<List<Post>?> GetPostsOfUsers(int userId);
    }
}
