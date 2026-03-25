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
    public class FavoriteRepository : GenericRepository<Favorite>, IFavoriteRepository
    {
        #region Filds
        private DbSet<Favorite> favorites;
        #endregion

        #region Constructor
        public FavoriteRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            favorites = applicationDbContext.Set<Favorite>();
        }

        #endregion

        #region Methods

        #endregion

    }
}
