using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Dashpoard.ServiceSeller.Queries.Results;

namespace Tradify.Core.Features.Dashpoard.ServiceSeller.Queries.Models
{
    public class GetServiceSellerDashboardQuery : IRequest<Response<GetServiceSellerDashboardResponse>>
    {
    }
}
