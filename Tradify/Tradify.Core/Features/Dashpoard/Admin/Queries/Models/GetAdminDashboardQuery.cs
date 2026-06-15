using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Dashpoard.Admin.Queries.Results;
using Tradify.Service.Services.Dashpoard;

namespace Tradify.Core.Features.Dashpoard.Admin.Queries.Models
{
    public class GetAdminDashboardQuery : IRequest<Response<GetAdminDashboardResponse>>
    {
    }
}
