using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Appointments;
using Tradify.Infrastructure.AbstractsRepositories;

namespace Tradify.Service.AbstractsServices
{
    public interface IEducationService :IEducationRepositorie
    {
        public Task<(string, int?)> AddEducationAsync(Education education);

    }
}
