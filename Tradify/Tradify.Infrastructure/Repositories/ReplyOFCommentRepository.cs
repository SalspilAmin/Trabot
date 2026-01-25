using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Comments;
using Tradify.Data.Entities.Posts;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;

namespace Tradify.Infrastructure.Repositories
{
    public class ReplyOFCommentRepository : GenericRepository<ReplyOFComment>, IReplyOFCommentRepository
    {
        #region Filds
        private DbSet<ReplyOFComment> ReplyOFComments;
        #endregion

        #region Constructor
        public ReplyOFCommentRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            ReplyOFComments = applicationDbContext.Set<ReplyOFComment>();
        }

        #endregion

        #region Methods

        #endregion
    }
}
