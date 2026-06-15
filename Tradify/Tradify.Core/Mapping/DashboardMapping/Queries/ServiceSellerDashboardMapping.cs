using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Dashpoard.Admin.Queries.Results;
using Tradify.Core.Features.Dashpoard.ServiceSeller.Queries.Results;
using Tradify.Service.Services.Dashpoard;

namespace Tradify.Core.Mapping.DashboardMapping
{

    public partial class DashpoardProfille
    {
        public void ServiceSellerDashboardMapping()
        {
            CreateMap<ServiceSellerDashboardDto, GetServiceSellerDashboardResponse>();

        }
    }
}
