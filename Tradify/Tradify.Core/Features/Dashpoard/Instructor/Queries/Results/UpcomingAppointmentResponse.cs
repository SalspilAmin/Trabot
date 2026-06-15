using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Features.Dashpoard.Instructor.Queries.Results
{
    public class UpcomingAppointmentResponse
    {
        public int BookingId { get; set; }

        public string CustomerName { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string AppointmentTime { get; set; }
    }
}
