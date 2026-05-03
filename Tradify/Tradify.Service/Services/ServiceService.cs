using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Data.Entities.Appointments;
using Tradify.Data.Entities.Identity;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;
using Microsoft.EntityFrameworkCore;

namespace Tradify.Service.Services
{
    public class ServiceService : Service<Data.Entities.Appointments.Service>, IServiceService
    {
        private readonly ICurrentUserService currentUserService;
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        private readonly IInstructorsService instructorsService;
        public ServiceService(IGenericRepository<Data.Entities.Appointments.Service> repository
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

        public async Task<(string, int?)> AddServiceAsync(Data.Entities.Appointments.Service service)
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


                    //8. check Duplicate serviceExists

                    var serviceExists = await context.Service
                              .AnyAsync(e => e.InstructorId == instructor.Id
                                        && e.Name == service.Name);

                    if (serviceExists)
                        return ("ServiceAlreadyExists", null);


                    //  1. Default values
                    service.InstructorId = instructor.Id;

                    // 2. Save
                    await AddAsync(service);
                    await SaveChangesAsync();


                    await transaction.CommitAsync();
                    return ("Success", service.Id);
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
