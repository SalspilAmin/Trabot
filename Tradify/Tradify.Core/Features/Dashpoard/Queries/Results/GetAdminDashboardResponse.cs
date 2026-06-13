using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Features.Dashpoard.Queries.Results
{
    public class GetAdminDashboardResponse
    {
        public int TotalUsers { get; set; }
        public int TotalSellers { get; set; }
        public int TotalInstructors { get; set; }
        public int TotalStores { get; set; }
        public int TotalProducts { get; set; }
        public int TotalServices { get; set; }
        public int TotalOrders { get; set; }
        public int TotalBookings { get; set; }

        public decimal TotalRevenue { get; set; }

        public decimal PendingPayouts { get; set; }

    }
}
