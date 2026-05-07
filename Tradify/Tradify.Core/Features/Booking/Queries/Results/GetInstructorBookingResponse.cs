using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Features.Booking.Queries.Results
{
    public  class GetInstructorBookingResponse
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public string? CustomerPhone { get; set; }

        public string BookingDate { get; set; }

        public string Day { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string BookingStatus { get; set; }
    }
}
