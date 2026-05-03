using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Appointments;
using Tradify.Infrastructure.AbstractsRepositories;

namespace Tradify.Service.AbstractsServices
{
    public interface ICertificationsService : ICertificationsRepositorie
    {
        public Task<(string, int?)> AddCertificationAsync(Certifications certification);

    }
}
