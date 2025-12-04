using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;

namespace Tradify.Infrastructure.Repositories
{
    public class ReviewRepository : GenericRepository<Reviews>, IReviewRepository
    {
        #region Filds
        private DbSet<Reviews> reviews;
        #endregion

        #region Constructor
        public ReviewRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            reviews = applicationDbContext.Set<Reviews>();
        }

        #endregion

        #region Methods

        #endregion

    }
}
