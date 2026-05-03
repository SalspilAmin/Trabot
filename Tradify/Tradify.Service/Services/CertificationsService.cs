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
    public class CertificationsService : Service<Certifications>, ICertificationsService
    {
        private readonly ICurrentUserService currentUserService;
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        private readonly IInstructorsService instructorsService;
        public CertificationsService(IGenericRepository<Certifications> repository
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


        public async Task<(string, int?)> AddCertificationAsync(Certifications certification)
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


                    //8. check Duplicate Certifications

                    var certificationExists = await context.Certifications
                              .AnyAsync(c => c.InstructorId == instructor.Id
                                        && c.Title == certification.Title);

                    if (certificationExists)
                        return ("CertificationAlreadyExists", null);


                    //  1. Default values
                    certification.InstructorId = instructor.Id;

                    // 2. Save
                    await AddAsync(certification);
                    await SaveChangesAsync();


                    await transaction.CommitAsync();
                    return ("Success", certification.Id);
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
