using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Posts;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Service.Services
{
    public class InteractionWithPostService : Service<InteractionWithPost>, IInteractionWithPostService
    {
        private readonly ApplicationDbContext context;




   

        public InteractionWithPostService(IGenericRepository<InteractionWithPost> repository, ApplicationDbContext context) : base(repository)
        {
            this.context = context;
        }

        public async Task<List<InteractionWithPost>> GetByPostIdAsync(int postId)
        {
            return await context
               .InteractionWithPosts

               .Include(x => x.User)

               .Where(x =>
                   x.PostId == postId &&
                   !x.IsDeleted)

               .OrderByDescending(x =>
                   x.CreatedAt)

               .ToListAsync();
        }

       

        public async Task<InteractionWithPost?> GetUserInteractionAsync(int userId, int postId)
        {
            return await context
                  .InteractionWithPosts

                  .Include(x => x.User)

                  .FirstOrDefaultAsync(x =>

                      x.UserId == userId &&

                      x.PostId == postId &&

                      !x.IsDeleted);
        }
    }
}
