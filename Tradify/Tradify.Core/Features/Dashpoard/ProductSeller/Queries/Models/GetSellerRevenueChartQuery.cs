using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Dashpoard.Admin.Queries.Results;

namespace Tradify.Core.Features.Dashpoard.ProductSeller.Queries.Models
{
    public class GetSellerRevenueChartQuery : IRequest<Response<List<RevenueChartResponse>>>
    {
    }
}
