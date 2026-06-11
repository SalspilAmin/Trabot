using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Education.Queries.Results;
using Tradify.Core.Features.ShipmentTrackings.Queries.Results;

namespace Tradify.Core.Features.ShipmentTrackings.Queries.Models
{
    public class GetShipmentTrackingByShipmentQuery : IRequest<List<GetShipmentTrackingByShipmentResponse>>
    {
        public int ShipmentId { get; set; } 
        public GetShipmentTrackingByShipmentQuery(int shipmentId)
        {
            ShipmentId = shipmentId;
        }
    }
}
