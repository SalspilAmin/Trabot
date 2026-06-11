using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Posts;
using Tradify.Infrastructure.AbstractsRepositories;

namespace Tradify.Service.AbstractsServices
{
    public interface IInteractionWithPostService : IInteractionWithPostRepository
    {
      

       public Task<List<InteractionWithPost>>GetByPostIdAsync(int postId);

       public Task<InteractionWithPost?>GetUserInteractionAsync(int userId,int postId);

      
    }
}
