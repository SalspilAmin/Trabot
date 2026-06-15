using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Dashpoard.Admin.Queries.Models;
using Tradify.Core.Features.Dashpoard.Admin.Queries.Results;
using Tradify.Core.Features.Dashpoard.ServiceSeller.Queries.Models;
using Tradify.Core.Features.Dashpoard.ServiceSeller.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Core.Wrappers;
using Tradify.Data.Enums;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;
using Tradify.Service.Services.Dashpoard;

namespace Tradify.Core.Features.Dashpoard.Admin.Queries.Handlers
{
    public class TopStoreItem
    {
        public int StoreId { get; set; }
        public int TotalCount { get; set; }
    }
    public class AdminDashpoardQueryHandler : ResponseHandler
                                                     , IRequestHandler<GetAdminDashboardQuery, Response<GetAdminDashboardResponse>>
                                                     , IRequestHandler<GetAdminOrdersChartQuery, Response<List<OrdersChartResponse>>>
                                                     , IRequestHandler<GetAdminRevenueChartQuery, Response<List<RevenueChartResponse>>>
                                                     , IRequestHandler<GetTopStoresQuery, Response<List<TopStoresResponse>>>
                                                     , IRequestHandler<GetAdminBookingsChartQuery, Response<List<BookingsChartResponse>>>





    {
        #region fields
        private readonly IMapper mapper;
        private readonly LocalizationService localization;
        private readonly IDashboardService dashboardService;
        private readonly IOrdersService ordersService;
        private readonly IPaymentService paymentService;
        private readonly IStoreService storeService;
        private readonly ISubOrderService subOrderService;
        private readonly IBookingsService bookingsService;


        #endregion

        #region Constructor

        public AdminDashpoardQueryHandler(LocalizationService localization,
            IMapper mapper
            , IDashboardService dashboardService
            , IOrdersService ordersService
            , IPaymentService paymentService
            , IStoreService storeService
            , ISubOrderService subOrderService,
IBookingsService bookingsService) : base(localization)
        {
            this.mapper = mapper;
            this.localization = localization;
            this.dashboardService = dashboardService;
            this.ordersService = ordersService;
            this.paymentService = paymentService;
            this.storeService = storeService;
            this.subOrderService = subOrderService;
            this.bookingsService = bookingsService;
        }


        public async Task<Response<GetAdminDashboardResponse>> Handle(GetAdminDashboardQuery request, CancellationToken cancellationToken)
        {


            var dashboard =
                await dashboardService.GetAdminDashboardAsync();

            var result = mapper.Map<GetAdminDashboardResponse>(dashboard);


            return Success<GetAdminDashboardResponse>(result);
        }

        public async Task<Response<List<OrdersChartResponse>>> Handle(GetAdminOrdersChartQuery request, CancellationToken cancellationToken)
        {


            var result = await ordersService.GetTableNoTracking()
     .GroupBy(x => x.CreatedAt.Month)
     .OrderBy(g => g.Key)
     .Select(g => new OrdersChartResponse
     {
         Month = CultureInfo.InvariantCulture
             .DateTimeFormat
             .GetAbbreviatedMonthName(g.Key),

         Count = g.Count()
     })
     .ToListAsync();

            return Success(result);
        }



        public async Task<Response<List<RevenueChartResponse>>> Handle(GetAdminRevenueChartQuery request, CancellationToken cancellationToken)
        {


            var result = await paymentService.GetTableNoTracking()
            .Where(x => x.PaymentStatus == PaymentStatus.Paid)
            .Include(x => x.Order)
            .GroupBy(x => x.Order.CreatedAt.Month)
            .OrderBy(g => g.Key)
            .Select(g => new RevenueChartResponse
            {
                Month = CultureInfo.InvariantCulture
                    .DateTimeFormat
                    .GetAbbreviatedMonthName(g.Key),

                Revenue = g.Sum(x => x.Amount)
            })
            .ToListAsync();

            return Success(result);
        }
       
        public async Task<Response<List<TopStoresResponse>>> Handle(GetTopStoresQuery request, CancellationToken cancellationToken)
        {

            List<TopStoreItem> topStores;

            if (request.storeType == StoreType.Product)
            {
                topStores = await subOrderService
    .GetTableNoTracking()
    .GroupBy(x => x.StoreId)
    .Select(g => new TopStoreItem
    {
        StoreId = g.Key,
        TotalCount = g.Count()
    })
    .OrderByDescending(x => x.TotalCount)
    .Take(4)
    .ToListAsync();
            }
            else
            {
                topStores = await bookingsService
                    .GetTableNoTracking()
                    .GroupBy(x => x.StoreId)
                    .Select(g => new TopStoreItem
                    {
                        StoreId = g.Key,
                        TotalCount = g.Count()
                    })
                    .OrderByDescending(x => x.TotalCount)
                    .Take(4)
                    .ToListAsync();
            }

            var storeIds = topStores.Select(x => x.StoreId).ToList();

            var stores = await storeService
                .GetTableNoTracking()
                .Where(x => storeIds.Contains(x.Id))
                .ToListAsync();

            var result = mapper.Map<List<TopStoresResponse>>(stores);

            foreach (var store in result)
            {
                store.TotalCount = topStores
                    .First(x => x.StoreId == store.Id)
                    .TotalCount;
            }

            result = result
                .OrderByDescending(x => x.TotalCount)
                .ToList();

            return Success(result);
        }

        public async Task<Response<List<BookingsChartResponse>>> Handle(GetAdminBookingsChartQuery request, CancellationToken cancellationToken)
        {
           

            var bookings = await bookingsService
     .GetTableNoTracking()
          .GroupBy(x => new
     {
         x.BookingDate.Year,
         x.BookingDate.Month
     })
     .Select(g => new
     {
         MonthNumber = g.Key.Month,
         TotalBookings = g.Count()
     })
     .ToListAsync();

            var result = bookings
                .OrderBy(x => x.MonthNumber)
                .Select(x => new BookingsChartResponse
                {
                    Month = CultureInfo.GetCultureInfo("en-US")
    .DateTimeFormat
    .GetAbbreviatedMonthName(x.MonthNumber),
                    TotalBookings = x.TotalBookings
                })
                .ToList();

            return Success(result);


        }



        #endregion

    }
}
