using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Dashpoard.Admin.Queries.Results;
using Tradify.Core.Features.Dashpoard.ProductSeller.Queries.Models;
using Tradify.Core.Features.Dashpoard.ProductSeller.Queries.Results;
using Tradify.Core.Features.Dashpoard.ServiceSeller.Queries.Models;
using Tradify.Core.Features.Dashpoard.ServiceSeller.Queries.Results;
using Tradify.Core.Features.Instructor.Queries.Results;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Core.Wrappers;
using Tradify.Data.Enums;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;

namespace Tradify.Core.Features.Dashpoard.ServiceSeller.Queries.Handlers
{
    public class SellerServiceDashpoardQueryHandller : ResponseHandler
                                                     , IRequestHandler<GetServiceSellerDashboardQuery, Response<GetServiceSellerDashboardResponse>>
                                                     , IRequestHandler<GetBookingsChartQuery, Response<List<BookingsChartResponse>>>
                                                     , IRequestHandler<GetTopInstructorsQuery, Response<PaginatedResult<TopInstructorResponse>>>

    {

        #region fields
        private readonly IMapper mapper;
        private readonly LocalizationService localization;
        private readonly IDashboardService dashboardService;
        private readonly ICurrentUserService currentUserService;
        private readonly IBookingsService bookingsService;
        private readonly IInstructorsService instructorsService;
       
        #endregion

        #region Constructor

        public SellerServiceDashpoardQueryHandller(LocalizationService localization,
            IMapper mapper
            , IDashboardService dashboardService,
              ICurrentUserService currentUserService,
              IBookingsService bookingsService,
              IInstructorsService instructorsService) : base(localization)
        {
            this.mapper = mapper;
            this.localization = localization;
            this.dashboardService = dashboardService;
            this.currentUserService = currentUserService;
            this.bookingsService = bookingsService;
            this.instructorsService = instructorsService;
        }


        public async Task<Response<GetServiceSellerDashboardResponse>> Handle(GetServiceSellerDashboardQuery request, CancellationToken cancellationToken)
        {
            var ValidSeller = await currentUserService.GetValidSellerContextAsync();

            if (ValidSeller.Error != null)
                return BadRequest<GetServiceSellerDashboardResponse>(localization.Get(ValidSeller.Error));

            var store = ValidSeller.Store;


            if (store.Type != StoreType.Service)
                return NotFound<GetServiceSellerDashboardResponse>(localization.Get("ThisDateNotAvilpaleForThisType"));

            var dashboard =
                await dashboardService.GetSellerServiceDashboardAsync(store.Id);

            var result = mapper.Map<GetServiceSellerDashboardResponse>(dashboard);


            return Success<GetServiceSellerDashboardResponse>(result);
        }


        public async Task<Response<List<BookingsChartResponse>>> Handle(GetBookingsChartQuery request, CancellationToken cancellationToken)
        {
            var ValidSeller = await currentUserService.GetValidSellerContextAsync();

            if (ValidSeller.Error != null)
                return BadRequest<List<BookingsChartResponse>>(localization.Get(ValidSeller.Error));

            var seller = ValidSeller.Seller;
            var store = ValidSeller.Store;


            if (store.Type != StoreType.Service)
                return NotFound<List<BookingsChartResponse>>(localization.Get("ThisDateNotAvilpaleForThisType"));


            var bookings = await bookingsService
     .GetTableNoTracking()
     .Where(x => x.StoreId == store.Id)
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

            var result= bookings
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


        public async Task<Response<PaginatedResult<TopInstructorResponse>>> Handle(GetTopInstructorsQuery request, CancellationToken cancellationToken)
        {
            var currentYear = DateTime.UtcNow.Year;

            var ValidSeller = await currentUserService.GetValidSellerContextAsync();

            if (ValidSeller.Error != null)
                return BadRequest<PaginatedResult<TopInstructorResponse>>(localization.Get(ValidSeller.Error));

            var seller = ValidSeller.Seller;
            var store = ValidSeller.Store;


            if (store.Type != StoreType.Service)
                return NotFound<PaginatedResult<TopInstructorResponse>>(localization.Get("ThisDateNotAvilpaleForThisType"));


            var query = instructorsService
                .GetTableNoTracking()
        .Where(i => i.StoreId == store.Id)
        .Select(i => new TopInstructorResponse
        {
            Id = i.Id,
            Name = i.Name,
            JobTitle = i.JobTitle,

            YearsOfExperience = i.YearsOfExperience,

            PricePerSession = i.PricePerSession,

            FinalPrice = i.FinalPrice,

            Discount = i.Discount,

            AverageRating = i.Reviews.Any()
                ? i.Reviews.Average(r => (double)r.Rating)
                : 0,

            ReviewsCount = i.Reviews.Count(),

            

            Image = i.InstructorImage == null
                ? null
                : new InstructorImageResponse
                {
                    Id = i.InstructorImage.Id,
                    MediaPath = i.InstructorImage.MediaPath
                },

            BookingCount = bookingsService.GetTableNoTracking().Count(b =>
                b.InstructorId == i.Id)
        })
         .Where(x => x.BookingCount > 0)
        .OrderByDescending(x => x.BookingCount);

            var result = await query.ToPaginationlist(
                request.PageNumber,
                request.PageSize);

            return Success(result);
        }

        #endregion

    }
}
