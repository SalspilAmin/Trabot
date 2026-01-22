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
    public class InteractionWithPostRepository : GenericRepository<InteractionWithPost>, IInteractionWithPostRepository
    {
        #region Filds
        private DbSet<InteractionWithPost> InteractionWithPosts;
        #endregion

        #region Constructor
        public InteractionWithPostRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            InteractionWithPosts = applicationDbContext.Set<InteractionWithPost>();
        }

        #endregion

        #region Methods

        #endregion
    }
}
