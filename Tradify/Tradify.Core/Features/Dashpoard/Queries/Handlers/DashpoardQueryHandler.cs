using AutoMapper;
using MediatR;
using System;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Dashpoard.Queries.Models;
using Tradify.Core.Features.Dashpoard.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Core.Wrappers;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;
using Tradify.Service.Services.Dashpoard;

namespace Tradify.Core.Features.Dashpoard.Queries.Handlers
{
    public class DashpoardQueryHandler : ResponseHandler
                                                     , IRequestHandler<GetAdminDashboardQuery, Response<GetAdminDashboardResponse>>

    {
        #region fields
        private readonly IMapper mapper;
        private readonly LocalizationService localization;
        private readonly IDashboardService dashboardService;
       



        #endregion

        #region Constructor

        public DashpoardQueryHandler(LocalizationService localization,
            IMapper mapper, IDashboardService dashboardService) : base(localization)
        {
            this.mapper = mapper;
            this.localization = localization;
            this.dashboardService = dashboardService;
        
        }


        public async Task<Response<GetAdminDashboardResponse>> Handle(GetAdminDashboardQuery request, CancellationToken cancellationToken)
        {


            var dashboard =
                await dashboardService.GetAdminDashboardAsync();

            var result = mapper.Map<GetAdminDashboardResponse>(dashboard);


            return Success<GetAdminDashboardResponse>(result);
        }

        #endregion

    }
}
