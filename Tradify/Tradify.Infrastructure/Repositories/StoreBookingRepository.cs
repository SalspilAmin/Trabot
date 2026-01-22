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
    public class StoreBookingRepository : GenericRepository<StoreBooking>, IStoreBookingRepository
    {
        #region Filds
        private DbSet<StoreBooking> StoreBookings;
        #endregion

        #region Constructor
        public StoreBookingRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            StoreBookings = applicationDbContext.Set<StoreBooking>();
        }

        #endregion

        #region Methods

        #endregion
    }
}
