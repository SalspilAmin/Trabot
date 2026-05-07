using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Features.Booking.Queries.Results
{
    public class GetBookingStatisticsResponse
    {
        public int TotalBookings { get; set; }

        public int Cancelled { get; set; }

        public int Completed { get; set; }
    }
}
