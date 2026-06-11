using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Order.Queries.Results;
using Tradify.Core.Features.ShipmentTrackings.Queries.Results;
using Tradify.Core.Wrappers;

namespace Tradify.Core.Features.ShipmentTrackings.Queries.Models
{
    public class GetSellerShipmentQuery : IRequest<Response<PaginatedResult<GetSellerShipmentResponse>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
