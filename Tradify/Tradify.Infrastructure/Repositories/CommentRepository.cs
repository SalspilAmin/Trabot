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
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        #region Filds
        private DbSet<Comment> Comments;
        #endregion

        #region Constructor
        public CommentRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            Comments = applicationDbContext.Set<Comment>();
        }

        #endregion

        #region Methods

        #endregion
    }
}
