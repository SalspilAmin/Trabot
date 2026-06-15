using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Service.Services.Dashpoard;

namespace Tradify.Service.AbstractsServices
{
    public interface IDashboardService
    {
        Task<AdminDashboardDto> GetAdminDashboardAsync();
        Task<SellerProductDashboardDto> GetSellerProducrDashboardAsync(int storeId);
        Task<ServiceSellerDashboardDto> GetSellerServiceDashboardAsync(int storeId);

        Task<InstructorDashboardDto> GetInstructorDashboardAsync(int instructorId);

    }
}
