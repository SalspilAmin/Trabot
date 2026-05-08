using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Booking.Command.Models
{
    public class RescheduleBookingCommand : IRequest<Response<string>>
    {
        public int BookingId { get; set; }

        public int NewScheduleId { get; set; }
    }
}
