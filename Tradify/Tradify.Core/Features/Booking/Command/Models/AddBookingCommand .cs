using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Booking.Command.Models
{
    public class AddBookingCommand : IRequest<Response<string>>
    {
        public int ScheduleId { get; set; }
    }
}
