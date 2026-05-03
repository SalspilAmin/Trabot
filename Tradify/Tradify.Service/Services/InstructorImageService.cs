using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Data.Entities.Appointments;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;

namespace Tradify.Service.Services
{
    public class InstructorImageService : Service<InstructorImage>, IInstructorImageService
    {
        public InstructorImageService(IGenericRepository<InstructorImage> repository) : base(repository)
        {
        }
    }
}
