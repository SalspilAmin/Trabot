using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Data.Entities.Identity;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;

namespace Tradify.Infrastructure.Repositories
{
    public class RefreshTokenRepository : GenericRepository<UserRefreshToken>,IRefreshTokenRepository
    {

        #region Filds
        private DbSet<UserRefreshToken> UserRefreshTokens;
        #endregion

        #region Constructor
        public RefreshTokenRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        #endregion


        #region Handle Functions

        #endregion
    }
}
