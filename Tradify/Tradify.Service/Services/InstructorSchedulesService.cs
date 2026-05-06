using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Appointments;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Enums;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;

namespace Tradify.Service.Services
{
    public class InstructorSchedulesService : Service<InstructorSchedules>, IInstructorSchedulesService
    {
        private readonly ICurrentUserService currentUserService;
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        private readonly IInstructorsService instructorsService;
        public InstructorSchedulesService(IGenericRepository<InstructorSchedules> repository
            , ICurrentUserService currentUserService
            , ApplicationDbContext context
            , UserManager<User> userManager
            , IInstructorsService instructorsService) : base(repository)
        {
            this.currentUserService = currentUserService;
            this.context = context;
            this.userManager = userManager;
            this.instructorsService = instructorsService;
        }


        public DateTime GetNextDate(DayOfWeek targetDay)
        {
            var today = DateTime.UtcNow.Date;

            int daysToAdd = ((int)targetDay - (int)today.DayOfWeek + 7) % 7;

            if (daysToAdd == 0)
                daysToAdd = 7;

            return today.AddDays(daysToAdd);
        }

        //public async Task<(int?, bool)> CheackInstructorAvalipalToday(int instructorId)
        //{
        //    var today = DateTime.UtcNow.DayOfWeek;

        //    // 1. Get schedules
        //    var schedules = await GetTableNoTracking()
        //        .Where(s => s.InstructorId==instructorId)   
        //        .ToListAsync();

        //    if (schedules.Count == 0)
        //        return (null,false);

        //    // 2. Dates
        //    var scheduleDates = schedules.ToDictionary(
        //        s => s.Id,
        //        s => GetNextDate(s.Day)
        //    );

        //    var targetDates = scheduleDates.Values
        //        .Select(d => d.Date)
        //        .Distinct()
        //        .ToList();

        //    // 3. Bookings
        //    var bookings = await bookingsService.GetTableNoTracking()
        //        .Where(b => targetDates.Contains(b.BookingDate.Date)
        //                 && b.Status != BookingStatus.Cancelled)
        //        .ToListAsync();

        //    // 4. Lookup
        //    var bookingsLookup = bookings
        //        .GroupBy(b => new { b.ScheduleId, Date = b.BookingDate.Date })
        //        .ToDictionary(g => g.Key, g => g.Count());

        //    // 5. Result
        //    var result = new Dictionary<int, bool>();

        //    foreach (var instructorId in instructorIds)
        //    {
        //        var instructorSchedules = schedules
        //            .Where(s => s.InstructorId == instructorId);

        //        var hasAvailable = instructorSchedules.Any(s =>
        //        {
        //            if (!s.IsAvailable || s.Day != today)
        //                return false;

        //            var date = scheduleDates[s.Id];

        //            var key = new { ScheduleId = s.Id, Date = date.Date };

        //            bookingsLookup.TryGetValue(key, out var booked);

        //            return (s.Capacity - booked) > 0;
        //        });

        //        result[instructorId] = hasAvailable;
        //    }

        //    return result;
        //}


















        public async Task<(string, int?)> AddInstructorSchedulesAsync(InstructorSchedules schedules)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    //  1. Get User 
                    var curntUserId = currentUserService.GetUserId();

                    var user = await context.Users.FirstOrDefaultAsync(u => u.Id == curntUserId);

                    if (user == null)
                        return ("UserNotFound", null);

                    if (user.IsDeleted)
                        return ("InstructorLinkedToDeletedUser", null);

                    //  2. Get instructor 

                    var instructor = await instructorsService.GetTableNoTracking()
                    .FirstOrDefaultAsync(i => i.UserId == curntUserId);

                    if (instructor == null)
                        return ("instructorNotFound", null);

                    if (!instructor.IsActive)
                        return ("instructorNotActive", null);

                    // 4. Check Overlapping
                    var hasConflict = await context.InstructorSchedules
                        .AnyAsync(s =>
                            s.InstructorId == instructor.Id &&
                            schedules.StartTime < s.EndTime &&
                            schedules.EndTime > s.StartTime
                        );

                    if (hasConflict)
                        return ("ScheduleConflict", null);

                    //8. check Duplicate schedulesExists

                    var schedulesExists = await context.InstructorSchedules
                              .AnyAsync(s => s.InstructorId == instructor.Id
                                        && s.StartTime == schedules.StartTime
                                        && s.EndTime == schedules.EndTime);

                    if (schedulesExists)
                        return ("schedulesExistsAlreadyExists", null);


                    //  1. Default values
                    schedules.InstructorId = instructor.Id;
                    schedules.IsAvailable = true;

                    // 2. Save
                    await AddAsync(schedules);
                    await SaveChangesAsync();


                    await transaction.CommitAsync();
                    return ("Success", schedules.Id);
                }

                catch (Exception ex)

                {

                    await transaction.RollbackAsync();
                    return ("Failed", null);

                }
            }
        }
    }
}
