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
    public class PayoutRepository : GenericRepository<Payouts>, IPayoutRepository
    {
        #region Filds
        private DbSet<Payouts> payouts;
        #endregion

        #region Constructor
        public PayoutRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            payouts = applicationDbContext.Set<Payouts>();
        }

        #endregion

        #region Methods

        #endregion

    }
}
