using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.ShipmentTrackings.Queries.Results;

namespace Tradify.Core.Features.ShipmentTrackings.Queries.Models
{
    public class GetShipmentByOrderIdQuery : IRequest<Response<GetShipmentByOrderIdResponse>>
    {
        public int SubOrderId { get; set; }

        public GetShipmentByOrderIdQuery(int subOrderId)
        {
            SubOrderId = subOrderId;
        }
    }
}
