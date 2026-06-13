using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Dashpoard.Queries.Results;
using Tradify.Core.Features.Education.Queries.Results;
using Tradify.Data.Entities.Appointments;
using Tradify.Service.Services.Dashpoard;

namespace Tradify.Core.Mapping.DashboardMapping
{

    public partial class DashpoardProfille : Profile
    {
        public void GetAdminDashboardMapping()
        {
            CreateMap<AdminDashboardDto, GetAdminDashboardResponse>();
        }
    }
}
