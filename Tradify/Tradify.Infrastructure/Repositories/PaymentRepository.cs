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
    public class PaymentRepository : GenericRepository<Payments>,IPaymentRepository
    {
        #region Filds
        private DbSet<Payments> payments;
        #endregion

        #region Constructor
        public PaymentRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            payments = applicationDbContext.Set<Payments>();
        }

        #endregion

        #region Methods

        #endregion
    
    }
}
