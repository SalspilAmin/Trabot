using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Data.Entities.Appointments;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Enums;
using Tradify.Data.Helpers;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;

namespace Tradify.Service.Services
{
    public class EducationService : Service<Education>, IEducationService
    {
        private readonly ICurrentUserService currentUserService;
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        private readonly IInstructorsService instructorsService;
        private readonly ILogger<EducationService> logger;

        public EducationService(IGenericRepository<Education> repository
            , ICurrentUserService currentUserService
            , ApplicationDbContext context
            , UserManager<User> userManager
            , IInstructorsService instructorsService ,
ILogger<EducationService> logger) : base(repository)
        {
            this.currentUserService = currentUserService;
            this.context = context;
            this.userManager = userManager;
            this.instructorsService = instructorsService;
            this.logger = logger;
        }


        public async Task<(string, int?)> AddEducationAsync(Education education)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    //  1. Get User 
                    var curntUserId =  currentUserService.GetUserId();

                    var user = await context.Users.FirstOrDefaultAsync(u => u.Id == curntUserId);

                    if (user == null)
                        return ("UserNotFound", null);

                    if (user.IsDeleted)
                        return ("InstructorLinkedToDeletedUser", null);

                    //  2. Get instructor 
                    Console.WriteLine(curntUserId);
                    var instructor = await instructorsService.GetTableNoTracking()
                    .FirstOrDefaultAsync(i => i.UserId == curntUserId);

                    if (instructor == null)
                        return ("instructorNotFound", null) ;

                    if (!instructor.IsActive)
                        return ( "instructorNotActive",null );


                    //8. check Duplicate EducationExists

                    var educationExists = await context.Education
                              .AnyAsync(e=>e.InstructorId == instructor.Id
                                        && e.Institution == education.Institution
                                        && e.Year == education.Year);

                    if (educationExists)
                        return ("EducationAlreadyExistsForThisYear", null);


                    //  1. Default values
                    education.InstructorId= instructor.Id;

                    // 2. Save
                    await AddAsync(education);
                    await SaveChangesAsync();


                    await transaction.CommitAsync();
                    return ("Success", education.Id);
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
