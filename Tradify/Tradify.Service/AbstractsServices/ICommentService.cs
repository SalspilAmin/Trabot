using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Comments;
using Tradify.Infrastructure.AbstractsRepositories;


namespace Tradify.Service.AbstractsServices
{
    public interface ICommentService : ICommentRepository
    {
     public Task<List<Comment>> GetCommentsByPostIdAsync(int postId);
    }
}
