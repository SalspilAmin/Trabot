using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Booking.Command.Models
{
    public class CancelBookingCommand : IRequest<Response<string>>
    {
        public int BookingId { get; set; }

        
    }
}
