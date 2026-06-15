using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Features.Dashpoard.ProductSeller.Queries.Results
{
    public class SellerProductDashboardResponse
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
}
