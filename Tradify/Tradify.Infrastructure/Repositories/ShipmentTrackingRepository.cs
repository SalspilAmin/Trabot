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
    public class ShipmentTrackingRepository : GenericRepository<ShipmentTracking>, IShipmentTrackingRepository
    {
        #region Filds
        private DbSet<ShipmentTracking> shipmentTrackings;
        #endregion

        #region Constructor
        public ShipmentTrackingRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            shipmentTrackings = applicationDbContext.Set<ShipmentTracking>();
        }

        #endregion

        #region Methods

        #endregion

    }
}
