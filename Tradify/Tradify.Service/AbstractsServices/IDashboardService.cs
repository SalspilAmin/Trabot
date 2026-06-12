using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Service.Services.Dashpoard;

namespace Tradify.Service.AbstractsServices
{
    public interface IDashboardService
    {
        Task<AdminDashboardDto> GetAdminDashboardAsync();
    }
}
