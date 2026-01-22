using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;

namespace Tradify.Service.Services
{
    public class AppointmentService : Service<Appointment>, IAppointmerntService
    {
        public AppointmentService(IGenericRepository<Appointment> repository) : base(repository)
        {
        }
    }
}
