using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Authorization.Queries.Results;

namespace Tradify.Core.Features.Authorization.Queries.Models
{
    public class GetRolesListQuery : IRequest<Response<List<GetRolesListResult>>>
    {
    }
}
