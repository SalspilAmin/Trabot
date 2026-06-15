using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Features.Dashpoard.Instructor.Queries.Results
{
    public class GetInstructorDashboardResponse
    {
        public int TotalCustomer { get; set; }

        public int TotalBookings { get; set; }

        public int CompletedBookings { get; set; }

        public int CancelledBookings { get; set; }

        public int TotalScheduals { get; set; }

        public int ActiveSchedual { get; set; }

        public int DisActiveSchedual { get; set; }

        public double AverageRating { get; set; }

        public int TotalReviews { get; set; }

        public int TotalCertifications { get; set; }

        public int TotalEducations { get; set; }
        public int TotalService { get; set; }


    }
}
