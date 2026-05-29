using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Chat;
using Tradify.Data.Entities.Comments;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;

namespace Tradify.Service.Services
{
    public class ReplyOFCommentService : Service<ReplyOFComment>, IReplyOFCommentService
    {
        private readonly ApplicationDbContext context;
        public ReplyOFCommentService(IGenericRepository<ReplyOFComment> repository,ApplicationDbContext context) : base(repository)
        {
            this.context = context;
        }

        public async Task<List<ReplyOFComment>> GetRepliesByCommentIdAsync(int commentId)
        {
            return await context.ReplyOFComments

          .Include(x =>
              x.UserThatWriteAReplyOFComment)

          .Where(x =>
              x.CommentId == commentId &&
              !x.IsDeleted)

          .OrderBy(x =>
              x.CreatedAt)

          .ToListAsync();
        }
    }
}
