using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Appointments;
using Tradify.Data.Entities.Identity;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;
using Microsoft.EntityFrameworkCore;

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
                    schedules.ReservedCount = 0;
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
