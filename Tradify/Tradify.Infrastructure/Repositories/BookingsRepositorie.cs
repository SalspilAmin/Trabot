using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Appointments;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;

namespace Tradify.Infrastructure.Repositories
{
    public class BookingsRepositorie : GenericRepository<Bookings>, IBookingsRepositorie
    {
        #region Filds
        private DbSet<Bookings> bookings;
        #endregion

        #region Constructor
        public BookingsRepositorie(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            bookings = applicationDbContext.Set<Bookings>();
        }

        #endregion

        #region Methods

        #endregion

    }
}
