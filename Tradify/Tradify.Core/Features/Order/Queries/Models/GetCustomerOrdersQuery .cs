using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Order.Queries.Results;
using Tradify.Core.Features.User.Queries.Results;
using Tradify.Core.Wrappers;

namespace Tradify.Core.Features.Order.Queries.Models
{
    public class GetCustomerOrdersQuery : IRequest<PaginatedResult<GetCustomerOrdersResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
