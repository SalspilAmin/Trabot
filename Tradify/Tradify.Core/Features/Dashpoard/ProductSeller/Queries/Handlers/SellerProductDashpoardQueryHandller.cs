using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Tradify.Core.Bases;
using Tradify.Core.Features.Dashpoard.Admin.Queries.Models;
using Tradify.Core.Features.Dashpoard.Admin.Queries.Results;
using Tradify.Core.Features.Dashpoard.ProductSeller.Queries.Models;
using Tradify.Core.Features.Dashpoard.ProductSeller.Queries.Results;
using Tradify.Core.Features.Product.Queries.Models;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Core.Wrappers;
using Tradify.Data.Entities;
using Tradify.Data.Enums;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Core.Features.Dashpoard.ProductSeller.Queries.Handlers
{
   

    public class SellerProductDashpoardQueryHandller : ResponseHandler
                                                     , IRequestHandler<GetSellerProductDashboardQuery, Response<SellerProductDashboardResponse>>
                                                     , IRequestHandler<GetSellerOrdersChartQuery, Response<List<OrdersChartResponse>>>
                                                     , IRequestHandler<GetSellerRevenueChartQuery, Response<List<RevenueChartResponse>>>
                                                     , IRequestHandler<GetTopProductsSellingQuery, Response<PaginatedResult<TopProductSellingResponse>>>
                                                     , IRequestHandler<GetTopRatedProductsQuery, Response<PaginatedResult<TopRatedProductResponse>>>

    {
        #region fields
        private readonly IMapper mapper;
        private readonly LocalizationService localization;
        private readonly IDashboardService dashboardService;
        private readonly ICurrentUserService currentUserService;
        private readonly ISubOrderService subOrderService;
        private readonly IPaymentService paymentService;
        private readonly IProductService productService;
        #endregion

        #region Constructor

        public SellerProductDashpoardQueryHandller(LocalizationService localization,
            IMapper mapper
            , IDashboardService dashboardService,
ICurrentUserService currentUserService , ISubOrderService subOrderService
            , IPaymentService paymentService, IProductService productService) : base(localization)
        {
            this.mapper = mapper;
            this.localization = localization;
            this.dashboardService = dashboardService;
            this.currentUserService = currentUserService;
            this.subOrderService = subOrderService;
            this.paymentService = paymentService;
            this.productService = productService;
        }


        public async Task<Response<SellerProductDashboardResponse>> Handle(GetSellerProductDashboardQuery request, CancellationToken cancellationToken)
        {
            var ValidSeller = await currentUserService.GetValidSellerContextAsync();

            if (ValidSeller.Error != null)
                return BadRequest<SellerProductDashboardResponse>(localization.Get(ValidSeller.Error));

            var seller = ValidSeller.Seller;
            var store = ValidSeller.Store;

           
            if (store.Type != StoreType.Product)
                return NotFound<SellerProductDashboardResponse>(localization.Get("ThisDateNotAvilpaleForThisType"));

            var dashboard =
                await dashboardService.GetSellerProducrDashboardAsync(store.Id);

            var result = mapper.Map<SellerProductDashboardResponse>(dashboard);


            return Success<SellerProductDashboardResponse>(result);
        }


        public async Task<Response<List<OrdersChartResponse>>> Handle(GetSellerOrdersChartQuery request, CancellationToken cancellationToken)
        {
            var ValidSeller = await currentUserService.GetValidSellerContextAsync();

            if (ValidSeller.Error != null)
                return BadRequest< List<OrdersChartResponse>>(localization.Get(ValidSeller.Error));

            var seller = ValidSeller.Seller;
            var store = ValidSeller.Store;


            if (store.Type != StoreType.Product)
                return NotFound<List<OrdersChartResponse>>(localization.Get("ThisDateNotAvilpaleForThisType"));


            var result = await subOrderService.GetTableNoTracking()
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

        public async Task<Response<List<RevenueChartResponse>>> Handle(GetSellerRevenueChartQuery request, CancellationToken cancellationToken)
        {

            var currentYear = DateTime.UtcNow.Year;

            var ValidSeller = await currentUserService.GetValidSellerContextAsync();

            if (ValidSeller.Error != null)
                return BadRequest<List<RevenueChartResponse>>(localization.Get(ValidSeller.Error));

            var seller = ValidSeller.Seller;
            var store = ValidSeller.Store;


            if (store.Type != StoreType.Product)
                return NotFound<List<RevenueChartResponse>>(localization.Get("ThisDateNotAvilpaleForThisType"));


            var result = await paymentService.GetTableNoTracking()
     .Where(x =>
         x.PaymentStatus == PaymentStatus.Paid &&
         x.StoreId == store.Id &&
         x.Order != null &&
         x.Order.CreatedAt.Year == currentYear)
     .GroupBy(x => x.Order!.CreatedAt.Month)
     .OrderBy(x => x.Key)
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





        public async Task<Response<PaginatedResult<TopProductSellingResponse>>> Handle(GetTopProductsSellingQuery request, CancellationToken cancellationToken)
        {
            var currentYear = DateTime.UtcNow.Year;

            var ValidSeller = await currentUserService.GetValidSellerContextAsync();

            if (ValidSeller.Error != null)
                return BadRequest<PaginatedResult<TopProductSellingResponse>>(localization.Get(ValidSeller.Error));

            var seller = ValidSeller.Seller;
            var store = ValidSeller.Store;


            if (store.Type != StoreType.Product)
                return NotFound<PaginatedResult<TopProductSellingResponse>>(localization.Get("ThisDateNotAvilpaleForThisType"));


            var query = productService
     .GetTableNoTracking()
     .Where(p => p.StoreId == store.Id)
     .Select(p => new TopProductSellingResponse
     {
         Id = p.Id,
         Name = p.Name,

         FinalPrice = p.ProductVariants.Any()
             ? p.ProductVariants.Min(v => v.FinalPrice)
             : 0,

         Rating = p.Reviews.Any()
             ? p.Reviews.Average(r => (double)r.Rating)
             : 0,

         ReviewsCount = p.Reviews.Count(),

         MainImage = p.ProductImages
             .Where(i => i.IsMain)
             .Select(i => new ProductImageResponse
             {
                 Id = i.Id,
                 MediaPath = i.MediaPath
             })
             .FirstOrDefault(),

         SalesCount = p.ProductVariants
             .SelectMany(v => v.OrderItems)
             .Where(oi => oi.SubOrder.StoreId == store.Id)
             .Sum(oi => (int?)oi.Quantity) ?? 0
     })
     .Where(x => x.SalesCount > 0)
     .OrderByDescending(x => x.SalesCount);

            var result = await query.ToPaginationlist(
                request.PageNumber,
                request.PageSize);

            return Success(result);
        }


        public async Task<Response<PaginatedResult<TopRatedProductResponse>>> Handle(
    GetTopRatedProductsQuery request,
    CancellationToken cancellationToken)
        {
            var validSeller = await currentUserService.GetValidSellerContextAsync();

            if (validSeller.Error != null)
                return BadRequest<PaginatedResult<TopRatedProductResponse>>(
                    localization.Get(validSeller.Error));

            var store = validSeller.Store;

            if (store.Type != StoreType.Product)
                return NotFound<PaginatedResult<TopRatedProductResponse>>(
                    localization.Get("ThisDateNotAvilpaleForThisType"));

            var query = productService
                .GetTableNoTracking()
                .Where(p => p.StoreId == store.Id)
                .Select(p => new TopRatedProductResponse
                {
                    Id = p.Id,

                    Name = p.Name,

                    FinalPrice = p.ProductVariants.Any()
                        ? p.ProductVariants.Min(v => v.FinalPrice)
                        : 0,

                    Rating = p.Reviews.Any()
                        ? p.Reviews.Average(r => (double)r.Rating)
                        : 0,

                    ReviewsCount = p.Reviews.Count(),

                    MainImage = p.ProductImages
                        .Where(i => i.IsMain)
                        .Select(i => new ProductImageResponse
                        {
                            Id = i.Id,
                            MediaPath = i.MediaPath
                        })
                        .FirstOrDefault()
                })
                .Where(x => x.ReviewsCount > 0)
                .OrderByDescending(x => x.Rating)
                .ThenByDescending(x => x.ReviewsCount);

            var result = await query.ToPaginationlist(
                request.PageNumber,
                request.PageSize);

            return Success(result);
        }


        #endregion

    }
}
