using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Education.Queries.Models;
using Tradify.Core.Features.Education.Queries.Results;
using Tradify.Core.Features.InstructorSchedules.Queries.Models;
using Tradify.Core.Features.InstructorSchedules.Queries.Results;
using Tradify.Core.Features.Store.Queries.Models;
using Tradify.Core.Features.Store.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Core.Wrappers;
using Tradify.Data.Enums;
using Tradify.Service.AbstractsServices;
using Twilio.TwiML.Voice;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Tradify.Core.Features.InstructorSchedules.Queries.Handlers
{
    public class InstructorSchedulesQueryHandler : ResponseHandler
                                                     , IRequestHandler<GetInstructorSchedulesQuery, List<GetInstructorSchedulesResponse>>


    {
        #region fields
        private readonly IMapper mapper;
        private readonly LocalizationService localization;
        private readonly IInstructorsService instructorsService;
        private readonly IInstructorSchedulesService instructorSchedulesService;
        private readonly IBookingsService bookingsService;
        #endregion

        #region Constructor
        public InstructorSchedulesQueryHandler(LocalizationService localization, IMapper mapper, IInstructorsService instructorsService
            , IInstructorSchedulesService instructorSchedulesService , IBookingsService bookingsService) : base(localization)
        {
            this.mapper = mapper;
            this.localization = localization;
            this.instructorsService = instructorsService;
            this.instructorSchedulesService = instructorSchedulesService;
            this.bookingsService = bookingsService; 
        }

        #endregion

        #region Mehtods


        // Get All Instructor Schedules List

        //public async Task<List<GetInstructorSchedulesResponse>> Handle(GetInstructorSchedulesQuery request, CancellationToken cancellationToken)
        //    {

        //        var schedules = await instructorSchedulesService.GetTableNoTracking()
        //            .Where(e => e.InstructorId == request.Id)
        //            .ToListAsync();

        //        if (!schedules.Any()) return new List<GetInstructorSchedulesResponse>();

        //        var resultList = new List<GetInstructorSchedulesResponse>();


        //        foreach (var schedule in schedules)
        //        {

        //            var bookingDate = instructorSchedulesService.GetNextDate(schedule.Day);


        //            var bookedCount = await bookingsService.GetTableNoTracking()
        //                .Where(x => x.ScheduleId == schedule.Id
        //                         && x.BookingDate.Date == bookingDate.Date
        //                         && x.Status != BookingStatus.Cancelled)
        //                .CountAsync();

        //            var availableSlots = schedule.Capacity - bookedCount;


        //            var mappedSchedule = mapper.Map<GetInstructorSchedulesResponse>(schedule);

        //            mappedSchedule.Available = availableSlots;
        //            mappedSchedule.Date = bookingDate;
        //            mappedSchedule.IsAvailable = availableSlots > 0 && schedule.IsAvailable;

        //            resultList.Add(mappedSchedule);
        //        }

        //        return resultList;
        //    }

        public async Task<List<GetInstructorSchedulesResponse>> Handle(GetInstructorSchedulesQuery request, CancellationToken cancellationToken)
        {
            // 1. Get schedules
            var schedules = await instructorSchedulesService.GetTableNoTracking()
                .Where(e => e.InstructorId == request.Id)
                .ToListAsync();

            if (schedules.Count == 0)
                return new List<GetInstructorSchedulesResponse>();

            // 2. Get next dates once
            var scheduleDates = schedules.ToDictionary(
                s => s.Id,
                s => instructorSchedulesService.GetNextDate(s.Day)
            );

            var targetDates = scheduleDates.Values
                .Select(d => d.Date)
                .Distinct()
                .ToList();

            // 3. Get bookings in ONE query
            var allBookings = await bookingsService.GetTableNoTracking()
                .Where(x => x.InstructorId == request.Id
                         && targetDates.Contains(x.BookingDate.Date)
                         && x.Status != BookingStatus.Cancelled)
                .ToListAsync();

            // 4. Build fast lookup
            var bookingsLookup = allBookings
                .GroupBy(x => new { x.ScheduleId, Date = x.BookingDate.Date })
                .ToDictionary(g => g.Key, g => g.Count());

            var resultList = new List<GetInstructorSchedulesResponse>();

            // 5. Loop (fast)
            foreach (var schedule in schedules)
            {
                var bookingDate = scheduleDates[schedule.Id];

                var key = new { ScheduleId = schedule.Id, Date = bookingDate.Date };

                bookingsLookup.TryGetValue(key, out var bookedCount);

                var availableSlots = Math.Max(0, schedule.Capacity - bookedCount);

                var mappedSchedule = mapper.Map<GetInstructorSchedulesResponse>(schedule);

                mappedSchedule.Available = availableSlots;
                mappedSchedule.Date = bookingDate.ToString("yyyy-MM-dd");
                mappedSchedule.IsAvailable = availableSlots > 0 && schedule.IsAvailable;

                resultList.Add(mappedSchedule);
            }

            return resultList;
        }


        #endregion
    }


}
