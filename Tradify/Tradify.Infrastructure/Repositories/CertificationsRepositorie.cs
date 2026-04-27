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
    public class CertificationsRepositorie : GenericRepository<Certifications>, ICertificationsRepositorie
    {
        #region Filds
        private DbSet<Certifications> certifications;
        #endregion

        #region Constructor
        public CertificationsRepositorie(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            certifications = applicationDbContext.Set<Certifications>();
        }

        #endregion

        #region Methods

        #endregion

    }
}
