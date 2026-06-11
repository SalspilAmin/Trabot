using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<InstructorSchedulesService> logger;

        public InstructorSchedulesService(IGenericRepository<InstructorSchedules> repository
            , ICurrentUserService currentUserService
            , ApplicationDbContext context
            , UserManager<User> userManager
            , IInstructorsService instructorsService
            , ILogger<InstructorSchedulesService> logger) : base(repository)
        {
            this.currentUserService = currentUserService;
            this.context = context;
            this.userManager = userManager;
            this.instructorsService = instructorsService;
            this.logger = logger;
        }


        public DateTime GetNextDate(DayOfWeek targetDay)
        {
            var today = DateTime.UtcNow.Date;

            int daysToAdd = ((int)targetDay - (int)today.DayOfWeek + 7) % 7;

            if (daysToAdd == 0)
                daysToAdd = 7;

            return today.AddDays(daysToAdd);
        }

        
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



                    // Validate time range
                    if (schedules.EndTime <= schedules.StartTime)
                        return ("InvalidTimeRange", null);

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
                                        && s.EndTime == schedules.EndTime
                                        &&s.Day==schedules.Day);

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
                    logger.LogError(ex, ex.Message);
                    throw;

                }
            }
        }
    }
}
