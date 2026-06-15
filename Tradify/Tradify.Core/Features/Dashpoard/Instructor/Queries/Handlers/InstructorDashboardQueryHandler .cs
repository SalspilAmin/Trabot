using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Dashpoard.Instructor.Queries.Models;
using Tradify.Core.Features.Dashpoard.Instructor.Queries.Results;
using Tradify.Core.Features.Dashpoard.ServiceSeller.Queries.Models;
using Tradify.Core.Features.Dashpoard.ServiceSeller.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Data.Enums;
using Tradify.Service.AbstractsServices;

namespace Tradify.Core.Features.Dashpoard.Instructor.Queries.Handlers
{
    public class InstructorDashboardQueryHandler : ResponseHandler
                                                     , IRequestHandler<GetInstructorDashboardQuery, Response<GetInstructorDashboardResponse>>
                                                     , IRequestHandler<GetInstructorSessionsChartQuery, Response<List<InstructorSessionsChartResponse>>>
                                                     , IRequestHandler<GetUpcomingAppointmentsQuery, Response<List<UpcomingAppointmentResponse>>>

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

        public InstructorDashboardQueryHandler(LocalizationService localization,
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

        #endregion

        #region Methods

        public async Task<Response<GetInstructorDashboardResponse>> Handle(GetInstructorDashboardQuery request, CancellationToken cancellationToken)
        {
           var cruntuserId=currentUserService.GetUserId();
            var instructor = await instructorsService.GetTableNoTracking()
                .FirstOrDefaultAsync(x=>x.UserId==cruntuserId);

            if (instructor==null)
                return NotFound<GetInstructorDashboardResponse>(localization.Get("InstructorNotFound"));

            var dashboard =
                await dashboardService.GetInstructorDashboardAsync(instructor.Id);

            var result = mapper.Map<GetInstructorDashboardResponse>(dashboard);


            return Success<GetInstructorDashboardResponse>(result);
        }


        public async Task<Response<List<InstructorSessionsChartResponse>>> Handle(GetInstructorSessionsChartQuery request, CancellationToken cancellationToken)
        {
            var cruntuserId = currentUserService.GetUserId();
            var instructor = await instructorsService.GetTableNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == cruntuserId);

            if (instructor == null)
                return NotFound<List<InstructorSessionsChartResponse>>(localization.Get("InstructorNotFound"));

            var result = await bookingsService
                .GetTableNoTracking()
                .Where(x => x.InstructorId == instructor.Id)
                .GroupBy(x => new
                {
                    x.BookingDate.Year,
                    x.BookingDate.Month
                })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    MonthNumber = g.Key.Month,
                    TotalSessions = g.Count()
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.MonthNumber)
                .ToListAsync();




            var response = result
     .Select(x => new InstructorSessionsChartResponse
     {
         Month = CultureInfo.CurrentCulture.DateTimeFormat
             .GetAbbreviatedMonthName(x.MonthNumber),
         TotalSessions = x.TotalSessions
     })
     .ToList();

            return Success(response);

        }

        public async Task<Response<List<UpcomingAppointmentResponse>>> Handle(GetUpcomingAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var cruntuserId = currentUserService.GetUserId();
            var instructor = await instructorsService.GetTableNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == cruntuserId);

            if (instructor == null)
                return NotFound<List<UpcomingAppointmentResponse>>(localization.Get("InstructorNotFound"));

            var bookings = await bookingsService.GetTableNoTracking()
                .Include(x => x.Customer)
    .Include(x => x.Schedule)
      .Where(x =>
          x.InstructorId == instructor.Id &&
          x.BookingDate >= DateTime.UtcNow)
      .OrderBy(x => x.BookingDate)
      .Take(10)
      .ToListAsync();

            var result = bookings.Select(x => new UpcomingAppointmentResponse
            {
                BookingId = x.Id,

                CustomerName = x.Customer.UserName,

                AppointmentDate = x.BookingDate,

                AppointmentTime =
    DateTime.Today.Add(x.Schedule.StartTime)
    .ToString("hh:mm tt")
            }).ToList();

            return Success(result);

        }

        #endregion
    }
}
