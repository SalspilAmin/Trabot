using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Enums;
using Tradify.Infrastructure.Context;
using Tradify.Service.AbstractsServices;

namespace Tradify.Service.Services.Dashpoard
{

   

    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;
        private readonly IStoreService storeService;
        private readonly IProductService productService;
        private readonly IProductVariantService productVariantService;
        private readonly ISubOrderService subOrderService;
        private readonly IShipmentService shipmentService;
        private readonly IPaymentService paymentService;
        private readonly IOrdersService ordersService;
        private readonly IReviewService reviewService;
        private readonly IServiceService serviceService;
        private readonly IInstructorsService instructorsService;
        private readonly IBookingsService bookingsService;
        private readonly IInstructorSchedulesService schedulesService;
        private readonly IEducationService educationService;
        private readonly ICertificationsService certificationsService;

        public DashboardService(
            ApplicationDbContext _context, IStoreService storeService
            , IProductService productService, IProductVariantService productVariantService
            , ISubOrderService subOrderService,IShipmentService shipmentService
            , IPaymentService paymentService, IOrdersService ordersService
            , IReviewService reviewService ,IServiceService serviceService 
            ,IInstructorsService instructorsService,IBookingsService bookingsService
            ,IInstructorSchedulesService schedulesService,IEducationService educationService
            ,ICertificationsService certificationsService)
        {
            this._context = _context;
            this.storeService = storeService;
            this.productService = productService;
            this.productVariantService = productVariantService;
            this.subOrderService = subOrderService;
            this.shipmentService = shipmentService;
            this.paymentService = paymentService;
            this.ordersService = ordersService;
            this.reviewService = reviewService;
            this.serviceService = serviceService;  
            this.instructorsService = instructorsService;
            this.bookingsService = bookingsService;
            this.schedulesService  = schedulesService;  
            this.educationService= educationService;    
            this.certificationsService = certificationsService; 
        }



        public async Task<AdminDashboardDto> GetAdminDashboardAsync()
        {
            var response = new AdminDashboardDto
            {
                TotalUsers = await _context.Users.CountAsync(),

                TotalSellers = await _context.Sellers.CountAsync(),

                TotalInstructors = await _context.Instructors.CountAsync(),

                TotalStores = await _context.Stores.CountAsync(),

                TotalProducts = await _context.Products.CountAsync(),

                TotalServices = await _context.Service.CountAsync(),

                TotalOrders = await _context.Orders.CountAsync(),

                TotalBookings = await _context.Bookings.CountAsync(),

                TotalRevenue = await _context.Payments
                    .Where(x => x.PaymentStatus == PaymentStatus.Paid)
                    .SumAsync(x => (decimal?)x.Amount) ?? 0,

                PendingPayouts = await _context.Payouts
                    .Where(x => x.PaymentStatus == PaymentStatus.Pending)
                    .SumAsync(x => (decimal?)x.Amount) ?? 0
            };

            return response;
        }


        public async Task<SellerProductDashboardDto> GetSellerProducrDashboardAsync(int storeId)
        {
            

            var totalProducts = await productService
                .GetTableNoTracking()
                .CountAsync(x => x.StoreId == storeId);

            var activeProducts = await productService
                .GetTableNoTracking()
                .CountAsync(x =>
                    x.StoreId == storeId &&
                    x.IsDeleted==false);

            var disActiveProducts = await productService
               .GetTableNoTracking()
               .IgnoreQueryFilters()
               .CountAsync(x =>
                   x.StoreId == storeId &&
                   x.IsDeleted == true);


            var totalProductsVariant = await productVariantService
               .GetTableNoTracking()
               .Include(v=>v.Product)
               .CountAsync(x => x.Product.StoreId == storeId);

            var activeProductsVariant = await productVariantService
                .GetTableNoTracking()
                .Include(v => v.Product)
                .CountAsync(x =>
                    x.Product.StoreId == storeId &&
                    x.IsDeleted == false);

            var disActiveProductsVariant = await productVariantService
               .GetTableNoTracking()
               .Include(v => v.Product)
               .IgnoreQueryFilters()
               .CountAsync(x =>
                   x.Product.StoreId == storeId &&
                   x.IsDeleted == true);


            var totalOrders = await subOrderService
                .GetTableNoTracking()
                .CountAsync(x => x.StoreId == storeId);

            var processingOrders = await subOrderService
                .GetTableNoTracking()
                .CountAsync(x =>
                    x.StoreId == storeId &&
                    x.Status == OrderStatus.processing);

            var cancelledOrders = await subOrderService
                .GetTableNoTracking()
                .CountAsync(x =>
                    x.StoreId == storeId &&
                    x.Status == OrderStatus.cancelled);

            var deliveredOrders = await subOrderService
                .GetTableNoTracking()
                .CountAsync(x =>
                    x.StoreId == storeId &&
                    x.Status == OrderStatus.delivered);

            var shippedOrders = await subOrderService
              .GetTableNoTracking()
              .CountAsync(x =>
                  x.StoreId == storeId &&
                  x.Status == OrderStatus.shipped);



            var shipmentsCount = await shipmentService
                .GetTableNoTracking()
                .Include(sh => sh.SubOrder)
                .CountAsync(x => x.SubOrder.StoreId == storeId);


            var shipmentsPending = await shipmentService
               .GetTableNoTracking()
               .Include(sh => sh.SubOrder)
               .CountAsync(x => x.SubOrder.StoreId == storeId &&
                  x.CurrentStatus == ShipmentStatus.Pending);


            var shipmentsShipped = await shipmentService
         .GetTableNoTracking()
         .Include(sh => sh.SubOrder)
         .CountAsync(x => x.SubOrder.StoreId == storeId &&
            x.CurrentStatus == ShipmentStatus.Shipped);


            var shipmentsDelivered = await shipmentService
                   .GetTableNoTracking()
                   .Include(sh => sh.SubOrder)
                   .CountAsync(x => x.SubOrder.StoreId == storeId &&
                      x.CurrentStatus == ShipmentStatus.Delivered);


            var shipmentsReturned = await shipmentService
                   .GetTableNoTracking()
                   .Include(sh => sh.SubOrder)
                   .CountAsync(x => x.SubOrder.StoreId == storeId &&
                      x.CurrentStatus == ShipmentStatus.Returned);



            var totalRevenue = await paymentService
                .GetTableNoTracking()
                .Where(x => x.StoreId == storeId)
                .SumAsync(x => (decimal?)x.Amount) ?? 0;

            var totalCustomers = await ordersService
                .GetTableNoTracking()
                .Where(o =>
                    o.subOrders.Any(s => s.StoreId == storeId))
                .Select(o => o.CustomerId)
                .Distinct()
                .CountAsync();

            var averageRating = await reviewService
                .GetTableNoTracking()
                .Include(r=>r.Product)
                .Where(r => r.Product.StoreId == storeId)
                .AverageAsync(r => (double?)r.Rating) ?? 0;

            var totalReviews = await reviewService
                .GetTableNoTracking()
                .Include(r => r.Product)

                .CountAsync(r => r.Product.StoreId == storeId);

            return new SellerProductDashboardDto
            {
                TotalProducts = totalProducts,
                ActiveProducts = activeProducts,
                DisActiveProducts= disActiveProductsVariant,
                TotalProductsVarints = totalProductsVariant,
                ActiveProductsVarints = activeProductsVariant,
                DisActiveProductsVarints = disActiveProductsVariant,
                TotalOrders = totalOrders,
                ProcessingOrders = processingOrders,
                CancelledOrders = cancelledOrders,
                DeliveredOrders = deliveredOrders,
                ShippedOrders= shippedOrders,
                ShipmentsCount= shipmentsCount,
                ShipmentsPending= shipmentsPending,
                ShipmentsShipped= shipmentsShipped,

                ShipmentsDelivered= shipmentsDelivered,

                ShipmentsReturned= shipmentsReturned,

                TotalRevenue = totalRevenue,

                TotalCustomers = totalCustomers,

                AverageRating = Math.Round(averageRating, 1),

                TotalReviews = totalReviews
            };
        }


        public async Task<ServiceSellerDashboardDto> GetSellerServiceDashboardAsync(int storeId)
        {
            var totalInstructor = await instructorsService
             .GetTableNoTracking()
             .CountAsync(x => x.StoreId == storeId);

            var totalActiveInstructor = await instructorsService
           .GetTableNoTracking()
           .CountAsync(x => x.StoreId == storeId&&x.IsActive==true);

            var totalDisActiveInstructor = await instructorsService
           .GetTableNoTracking()
           .IgnoreQueryFilters()
           .CountAsync(x => x.StoreId == storeId && x.IsActive == false);

            var totalServices = await serviceService
                .GetTableNoTracking()
                .Include(s=>s.Instructor)
                .CountAsync(x => x.Instructor.StoreId == storeId);


            var totalBookings = await bookingsService
                .GetTableNoTracking()
                .CountAsync(x => x.StoreId == storeId);

            var completedBookings = await bookingsService
                .GetTableNoTracking()
                .CountAsync(x =>
                    x.StoreId == storeId &&
                    x.Status == BookingStatus.Completed);

            var cancelledBookings = await bookingsService
                .GetTableNoTracking()
                .CountAsync(x =>
                    x.StoreId == storeId &&
                    x.Status == BookingStatus.Cancelled);


           

            var averageRating = await reviewService
                .GetTableNoTracking()
                .Include(r => r.Instructor)
                .Where(r => r.Instructor.StoreId == storeId)
                .AverageAsync(r => (double?)r.Rating) ?? 0;

            var totalReviews = await reviewService
                .GetTableNoTracking()
                .Include(r => r.Instructor)

                .CountAsync(r => r.Instructor.StoreId == storeId);


            var totalCustomers = await bookingsService
               .GetTableNoTracking()
               .Where(o =>
                   o.StoreId == storeId)
               .Select(o => o.CustomerId)
               .Distinct()
               .CountAsync();

            return new ServiceSellerDashboardDto
            {
                TotalInstructor= totalInstructor,
                TotalActiveInstructor= totalActiveInstructor,
                TotalDisActiveInstructor= totalDisActiveInstructor,

                TotalServices = totalServices,
                TotalBookings = totalBookings,
                CompletedBookings = completedBookings,
                CancelledBookings = cancelledBookings,
                AverageRating = averageRating,
                TotalReviews = totalReviews,
                TotalCustomers= totalCustomers
            };
        }


        public async Task<InstructorDashboardDto>GetInstructorDashboardAsync(int instructorId)
        {
            var instructor = await instructorsService.GetTableNoTracking()
                .Include(x => x.Reviews)
                .Include(x => x.Certifications)
                .FirstOrDefaultAsync(x => x.Id == instructorId);

            if (instructor == null)
                return null;

            var totalBookings = await bookingsService.GetTableNoTracking()  
                .CountAsync(x => x.InstructorId == instructorId);

            var completedBookings = await bookingsService.GetTableNoTracking()
                .CountAsync(x =>
                    x.InstructorId == instructorId &&
                    x.Status == BookingStatus.Completed);

            var cancelledBookings = await bookingsService.GetTableNoTracking()
                .CountAsync(x =>
                    x.InstructorId == instructorId &&
                    x.Status == BookingStatus.Cancelled);

            var totalCustomers = await bookingsService.GetTableNoTracking()
                .Where(x => x.InstructorId == instructorId)
                .Select(x => x.CustomerId)
                .Distinct()
                .CountAsync();

            var totalScheduals = await schedulesService.GetTableNoTracking()
               .CountAsync(x =>
                   x.InstructorId == instructorId);

            var activeSchedual = await schedulesService.GetTableNoTracking()
               .CountAsync(x =>
                   x.InstructorId == instructorId &&
                   x.IsAvailable == true);

            var disActiveSchedual = await schedulesService.GetTableNoTracking()
                .IgnoreQueryFilters()
               .CountAsync(x =>
                   x.InstructorId == instructorId &&
                   x.IsAvailable == false);

            var totalEducations = await educationService.GetTableNoTracking()
               .CountAsync(x =>
                   x.InstructorId == instructorId);

            var totalService = await serviceService.GetTableNoTracking()
               .CountAsync(x =>
                   x.InstructorId == instructorId);


            var totalCertifications = await certificationsService.GetTableNoTracking()
               .CountAsync(x =>
                   x.InstructorId == instructorId);

            return new InstructorDashboardDto
            {
                TotalCustomer = totalCustomers,


                TotalBookings = totalBookings,

                CompletedBookings = completedBookings,

                CancelledBookings = cancelledBookings,
                TotalScheduals=totalScheduals,
                ActiveSchedual=activeSchedual,
                DisActiveSchedual=disActiveSchedual,
                TotalService=totalService,  
                TotalEducations = totalEducations,

                AverageRating = instructor.Reviews.Any()
                    ? instructor.Reviews.Average(x => (double)x.Rating)
                    : 0,

                TotalReviews = instructor.Reviews?.Count ?? 0,

                TotalCertifications = instructor.Certifications?.Count ?? 0
            };
        }
    }

}
