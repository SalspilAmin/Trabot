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
    public class InstructorImageRepositories : GenericRepository<InstructorImage>, IInstructorImageRepositories
    {
        #region Filds
        private DbSet<InstructorImage> instructorImage;
        #endregion

        #region Constructor
        public InstructorImageRepositories(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            instructorImage = applicationDbContext.Set<InstructorImage>();
        }

        #endregion

        #region Methods

        #endregion

    }
}
