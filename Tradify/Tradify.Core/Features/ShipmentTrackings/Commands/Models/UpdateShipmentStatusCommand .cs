using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Data.Enums;

namespace Tradify.Core.Features.ShipmentTrackings.Commands.Models
{
    public class UpdateShipmentStatusCommand : IRequest<Response<string>>
    {
        public int ShipmentTrackingId { get; set; }

        public ShipmentStatus Status { get; set; }

    }
}
