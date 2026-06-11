using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.ShipmentTrackings.Commands.Models
{
    public class CreateShipmentCommand : IRequest<Response<string>>
    {
        public int SubOrderId { get; set; }

        public CreateShipmentCommand (int subOrderId)
        {
            SubOrderId = subOrderId;
        }
    }
}
