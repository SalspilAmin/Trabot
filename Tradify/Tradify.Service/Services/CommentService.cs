using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Chat;
using Tradify.Data.Entities.Comments;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;
using Microsoft.EntityFrameworkCore;

namespace Tradify.Service.Services
{
    public class CommentService : Service<Comment>, ICommentService
    {
        private readonly ApplicationDbContext context;

        public CommentService(IGenericRepository<Comment> repository,ApplicationDbContext context) : base(repository)
        {
            this.context = context;
        }

        public async Task<List<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            return await context.Comments

        .Include(x => x.User)

        .Include(x => x.ReplyOFComments)

        .Where(x =>
            x.PostId == postId &&
            !x.IsDeleted)

        .OrderByDescending(x =>
            x.CreatedAt)

        .ToListAsync();
        }
    }
}
