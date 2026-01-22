using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Data.Entities.Posts;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;

namespace Tradify.Infrastructure.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        #region Filds
        private DbSet<Post> Posts;
        #endregion

        #region Constructor
        public PostRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            Posts = applicationDbContext.Set<Post>();
        }

        #endregion

        #region Methods

        #endregion
    }
}
