using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Appointments;
using Tradify.Infrastructure.AbstractsRepositories;

namespace Tradify.Service.AbstractsServices
{
    public interface IInstructorSchedulesService :IInstructorSchedulesRepositorie
    {
        public Task<(string, int?)> AddInstructorSchedulesAsync(InstructorSchedules schedules);

    }
}
