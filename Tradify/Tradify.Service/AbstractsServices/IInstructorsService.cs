using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Appointments;
using Tradify.Infrastructure.AbstractsRepositories;

namespace Tradify.Service.AbstractsServices
{
    public interface IInstructorsService :IInstructorsRepositorie
    {
        public  Task<(string, int?)> AddInstructorAsync(Instructors instructors ,int UserId);
    }
}
