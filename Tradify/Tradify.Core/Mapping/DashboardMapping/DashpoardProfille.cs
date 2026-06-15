using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Mapping.DashboardMapping
{
    
        public partial class DashpoardProfille : Profile
        {
            public DashpoardProfille()
            {
            GetAdminDashboardMapping();
            SellerProductDashpoardMapping();
            ServiceSellerDashboardMapping();
            GetInstructorDashboardMapping();
            }
        }
    
}
