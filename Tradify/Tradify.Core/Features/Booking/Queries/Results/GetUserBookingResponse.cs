using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Enums;

namespace Tradify.Core.Features.Booking.Queries.Results
{
    public class GetUserBookingResponse
    {
        public int Id { get; set; }

        public string Day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public string BookingStatus { get; set; }

        public string BookingDate { get; set; } 

        public string StoreName { get; set; }
        public string InstructorName { get; set; }
        public int InstructorId { get; set; }   

    }
}
