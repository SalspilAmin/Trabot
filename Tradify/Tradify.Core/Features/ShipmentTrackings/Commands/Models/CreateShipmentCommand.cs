using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.ShipmentTrackings.Commands.Models
{
    public class CreateShipmentCommand : IRequest<Response<string>>
    {
        public int OrderId { get; set; }
        public CreateShipmentCommand(int orderId)
        {  OrderId = orderId;   }
    }
}
