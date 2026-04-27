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
    public class ServiceRepositorie : GenericRepository<Service>, IServiceRepositorie
    {
        #region Filds
        private DbSet<Service> service;
        #endregion

        #region Constructor
        public ServiceRepositorie(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            service = applicationDbContext.Set<Service>();
        }

        #endregion

        #region Methods

        #endregion

    }
}
