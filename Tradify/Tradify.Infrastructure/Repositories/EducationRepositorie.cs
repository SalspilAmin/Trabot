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
    public class EducationRepositorie : GenericRepository<Education>, IEducationRepositorie
    {
        #region Filds
        private DbSet<Education> education;
        #endregion

        #region Constructor
        public EducationRepositorie(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            education = applicationDbContext.Set<Education>();
        }

        #endregion

        #region Methods

        #endregion

    }
}
