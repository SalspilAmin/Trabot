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
    public class InstructorSchedulesRepositorie : GenericRepository<InstructorSchedules>, IInstructorSchedulesRepositorie
    {
        #region Filds
        private DbSet<InstructorSchedules> instructorSchedules;
        #endregion

        #region Constructor
        public InstructorSchedulesRepositorie(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            instructorSchedules = applicationDbContext.Set<InstructorSchedules>();
        }

        #endregion

        #region Methods

        #endregion

    }
}
