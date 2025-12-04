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
    public class ShipmentRepository : GenericRepository<Shipments>, IShipmentRepository
    {
        #region Filds
        private DbSet<Shipments> shipments;
        #endregion

        #region Constructor
        public ShipmentRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            shipments = applicationDbContext.Set<Shipments>();
        }

        #endregion

        #region Methods

        #endregion

    }
}
