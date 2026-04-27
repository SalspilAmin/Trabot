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
    public class InstructorsRepositorie : GenericRepository<Instructors>, IInstructorsRepositorie
    {
        #region Filds
        private DbSet<Instructors> instructors;
        #endregion

        #region Constructor
        public InstructorsRepositorie(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            instructors = applicationDbContext.Set<Instructors>();
        }

        #endregion

        #region Methods

        #endregion

    }
}
