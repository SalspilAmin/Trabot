using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Infrastructure.Repositories;

namespace Tradify.Service.AbstractsServices
{
    public interface IServiceService : IServiceRepositorie
    {
        public Task<(string, int?)> AddServiceAsync(Data.Entities.Appointments.Service service);

    }
}
