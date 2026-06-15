using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Features.Dashpoard.ServiceSeller.Queries.Results
{
    public class GetServiceSellerDashboardResponse
    {
        public int TotalInstructor { get; set; }
        public int TotalActiveInstructor { get; set; }
        public int TotalDisActiveInstructor { get; set; }
        public int TotalCustomers { get; set; }

        public int TotalServices { get; set; }

        public int TotalBookings { get; set; }

        public int CompletedBookings { get; set; }

        public int CancelledBookings { get; set; }

        public double AverageRating { get; set; }

        public int TotalReviews { get; set; }
    }
}
