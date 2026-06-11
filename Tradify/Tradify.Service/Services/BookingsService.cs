using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Data.Entities.Appointments;
using Tradify.Data.Enums;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;
using Microsoft.Extensions.Logging;
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Service.Services
{
    public class BookingsService : Service<Bookings>, IBookingsService
    {
        private readonly ICurrentUserService currentUserService;
        private readonly IInstructorSchedulesService instructorSchedulesService;
        private readonly ApplicationDbContext context;
        private readonly ILogger<BookingsService> logger;
        public BookingsService(IGenericRepository<Bookings> repository
            , ICurrentUserService currentUserService
            , IInstructorSchedulesService instructorSchedulesService
            , ApplicationDbContext context
            , ILogger<BookingsService> logger) : base(repository)
        {
            this.currentUserService = currentUserService;
            this.instructorSchedulesService = instructorSchedulesService; 
            this.context = context;
            this.logger = logger;
        }

        public async Task<bool> IsInstructorAvailableToday(int instructorId)
        {
            var today = DateTime.UtcNow.DayOfWeek;

            // 1. Get Instructor Schadule
            var schedules = await instructorSchedulesService.GetTableNoTracking()
                .Where(s => s.InstructorId == instructorId)
                .ToListAsync();

            if (!schedules.Any())
                return false;

            // 2. Count the realey date for days 
            var scheduleDates = schedules.ToDictionary(
                s => s.Id,
                s => instructorSchedulesService.GetNextDate(s.Day)
            );

            var targetDates = scheduleDates.Values
                .Select(d => d.Date)
                .Distinct()
                .ToList();

            // 3. Get Booking 
            var bookings = await GetTableNoTracking()
                .Where(b => targetDates.Contains(b.BookingDate.Date)
                         && b.Status != BookingStatus.Cancelled)
                .ToListAsync();


            var bookingsLookup = bookings
                .GroupBy(b => new { b.ScheduleId, Date = b.BookingDate.Date })
                .ToDictionary(g => g.Key, g => g.Count());

            // 5. Is Avilpal Today
            foreach (var schedule in schedules)
            {
                // skip
                if (!schedule.IsAvailable || schedule.Day != today)
                    continue;

                var date = scheduleDates[schedule.Id];

                var key = new { ScheduleId = schedule.Id, Date = date.Date };

                bookingsLookup.TryGetValue(key, out var booked);

                var availableSlots = schedule.Capacity - booked;

                if (availableSlots > 0)
                    return true;
            }

            return false;
        }


        public async Task<(string, int?)> AddBookingAsync(Bookings booking)
        {
            using (var transaction = await context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable))
            {
                try
                {
                    //  1. Get User 
                    var currentUserId = currentUserService.GetUserId();

                    var user = await context.Users.FirstOrDefaultAsync(u => u.Id == currentUserId);

                    if (user == null)
                        return ("UserNotFound", null);

                    if (user.IsDeleted)
                        return ("UserIsDeleted", null);

                    //  2. Get Schadual 

                    var schedule = await instructorSchedulesService.GetTableNoTracking()
                    .Include(i => i.Instructor)
                    .FirstOrDefaultAsync(i => i.Id == booking.ScheduleId);

                    if (schedule == null)
                        return ("instructorNotFound", null);

                    if (!schedule.IsAvailable)
                        return ("ThisScheduleIsNotAvailable",null);
                    // Check If Instructor Booking 

                    if (schedule.Instructor.UserId == currentUserId)
                        return ("YouCan'tBookingYourSelf", null);




                    // get booking date
                    var bookingDate = instructorSchedulesService.GetNextDate(schedule.Day);

                    // count current bookings
                    var bookedCount = await GetTableNoTracking()
                        .CountAsync(x =>
                            x.ScheduleId == booking.ScheduleId &&
                            x.BookingDate.Date == bookingDate.Date &&
                            x.Status != BookingStatus.Cancelled);

                    var availableSeats = Math.Max(0, schedule.Capacity - bookedCount);

                    if (availableSeats <= 0)
                        return ("NoAvailableSlotsRemaining", null);
                    // Cheack Reapet Booking
                    var alreadyBooked = await GetTableNoTracking()
                           .AnyAsync(x =>
                                  x.CustomerId == currentUserId &&
                                  x.ScheduleId == booking.ScheduleId &&
                                  x.BookingDate.Date == bookingDate.Date &&
                                  x.Status != BookingStatus.Cancelled);

                    if (alreadyBooked)
                        return ("AlreadyBookedThisSchedule", null);


                    //  1. Default values
                    booking.InstructorId = schedule.InstructorId;
                    booking.ScheduleId = schedule.Id;
                    booking.CustomerId = currentUserId;
                    booking.StoreId = schedule  .Instructor.StoreId;
                    booking.BookingDate = bookingDate;
                    booking.CreatedAt = DateTime.UtcNow;
                    booking.Status = BookingStatus.Completed;


                    // 2. Save
                    await AddAsync(booking);
                    await SaveChangesAsync();


                    await transaction.CommitAsync();
                    return ("Success", booking.Id);
                }

                catch (Exception ex)

                {

                    await transaction.RollbackAsync();
                    logger.LogError(ex, ex.Message);
                    throw;

                }
            }
        }



        // RescheduleBookingAsync

        public async Task<string> RescheduleBookingAsync(int bookingId, int newScheduleId)
        {
            using (var transaction = await context.Database
                .BeginTransactionAsync(System.Data.IsolationLevel.Serializable))
            {
                try
                {
                    //  1. Get User 
                    var currentUserId = currentUserService.GetUserId();

                    var user = await context.Users.FirstOrDefaultAsync(u => u.Id == currentUserId);

                    if (user == null)
                        return "UserNotFound";

                    if (user.IsDeleted)
                        return "UserIsDeleted";

                    // Old Booking
                    var booking = await GetTableAsTracking()
                        .Include(x => x.Instructor)
                        .FirstOrDefaultAsync(x => x.Id == bookingId);

                    if (booking == null)
                        return "BookingNotFound";

                    if (booking.CustomerId != currentUserId)
                        return "Unauthorized";

                    if (booking.Status == BookingStatus.Cancelled)
                        return "BookingAlreadyCancelled";

                    if (booking.BookingDate <= DateTime.UtcNow)
                        return "AppointmentAlreadyStarted";

                    // New Schedule
                    var newSchedule = await instructorSchedulesService
                        .GetTableNoTracking()
                        .Include(x => x.Instructor)
                        .FirstOrDefaultAsync(x => x.Id == newScheduleId);

                    if (newSchedule == null)
                        return "ScheduleNotFound";
                    if (newSchedule.Id== booking.ScheduleId)
                        return "AlreadyBookedThisSchedule";

                    if (booking.InstructorId != newSchedule.InstructorId)
                        return "ThisScheduleNotFortheSameInstructor";

                    if (!newSchedule.IsAvailable)
                        return "ScheduleNotAvailable";

                    // Prevent booking yourself
                    if (newSchedule.Instructor.UserId == currentUserId)
                        return "YouCantBookYourself";

                    // New Booking Date
                    var newBookingDate =
                        instructorSchedulesService.GetNextDate(newSchedule.Day);



                    if (booking.ScheduleId == newSchedule.Id &&booking.BookingDate.Date == newBookingDate.Date)
                    {
                        return "AlreadyBookedThisSchedule";
                    }
                    // Check capacity
                    var bookedCount = await GetTableNoTracking()
                    .CountAsync(x =>
                    x.Id != bookingId &&
        x.ScheduleId == newScheduleId &&
        x.BookingDate.Date == newBookingDate.Date &&
        x.Status != BookingStatus.Cancelled);

                    var availableSeats =
                        Math.Max(0, newSchedule.Capacity - bookedCount);

                    if (availableSeats <= 0)
                        return "NoAvailableSlotsRemaining";
                   
                    // Update booking
                    booking.ScheduleId = newSchedule.Id;

                    booking.BookingDate = newBookingDate;

                    booking.InstructorId = newSchedule.InstructorId;

                    booking.StoreId = newSchedule.Instructor.StoreId;

                    await UpdateAsync(booking);

                    await SaveChangesAsync();

                    await transaction.CommitAsync();

                    return "Success";
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    logger.LogError(ex, ex.Message);

                    throw;
                }
            }
        }





    }
}
