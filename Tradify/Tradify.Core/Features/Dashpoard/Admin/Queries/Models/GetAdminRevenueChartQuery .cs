using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Dashpoard.Admin.Queries.Results;

namespace Tradify.Core.Features.Dashpoard.Admin.Queries.Models
{
    public class GetAdminRevenueChartQuery : IRequest<Response<List<RevenueChartResponse>>>
    {
    }
}
