using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Service.Services.Dashpoard
{
    public class AdminDashboardDto
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


    public class SellerProductDashboardDto
    {
        public int TotalProducts { get; set; }
        public int ActiveProducts { get; set; }
        public int DisActiveProducts { get; set; }
        public int TotalProductsVarints { get; set; }
        public int ActiveProductsVarints { get; set; }
        public int DisActiveProductsVarints { get; set; }
        public int TotalOrders { get; set; }
        public int ProcessingOrders { get; set; }
        public int CancelledOrders { get; set; }
        public int DeliveredOrders { get; set; }
        public int ShippedOrders { get; set; }
        public int ShipmentsCount { get; set; }

        public int ShipmentsPending { get; set; }
        public int ShipmentsShipped { get; set; }

        public int ShipmentsDelivered { get; set; } 

        public int ShipmentsReturned { get; set; }  

        public int PendingOrders { get; set; }
        public int CompletedOrders { get; set; }

        public decimal TotalRevenue { get; set; }

        public int TotalCustomers { get; set; }

        public double AverageRating { get; set; }

        public int TotalReviews { get; set; }
    }

    public class ServiceSellerDashboardDto
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


    public class InstructorDashboardDto
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
