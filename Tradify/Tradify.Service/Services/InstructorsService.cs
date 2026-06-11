using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Data.Entities.Appointments;
using Tradify.Data.Entities.Comments;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Enums;
using Tradify.Data.Helpers;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Service.Services
{
    public class InstructorsService : Service<Instructors>, IInstructorsService
    {
        private readonly ICurrentUserService currentUserService;
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        private readonly ILogger<InstructorsService> logger;


        public InstructorsService(IGenericRepository<Instructors> repository
            , ICurrentUserService currentUserService
            , ApplicationDbContext context
            , UserManager<User> userManager
            , ILogger<InstructorsService> logger) : base(repository)
        {
            this.currentUserService = currentUserService;
            this.context = context;
            this.userManager = userManager;
            this.logger = logger;   
        }


        public async Task<(string, int?)> AddInstructorAsync(Instructors instructors ,int UserId)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    //  1. Check if seller exist
                    var ValidSeller = await currentUserService.GetValidSellerContextAsync();

                    if (ValidSeller.Error != null)
                        return (ValidSeller.Error, null);

                    // 2. Get Seller , Store
                    var seller = ValidSeller.Seller;
                    var store = ValidSeller.Store;

                    //3. Cheack If Store Type Is Service 

                    if (store.Type != StoreType.Service)

                        return ("ThisActionAllowedForServiceStoresOnly", null);

                    //5. Check if user exist
                    var user = await context.Users.FirstOrDefaultAsync(u => u.Id == UserId);

                    if (user == null)
                    {
                        return ("UserNotFound", null);
                    }

                    //6. Check if user is Dleated 
                    if (user.IsDeleted)
                        return ("UserDeleted", null);



                    //7. Check is user already Instructor
                    var existingInstructor = await GetTableNoTracking()
                    .FirstOrDefaultAsync(I => I.UserId == UserId);

                    if (existingInstructor != null)
                        return ("UserIsAlreadyInstructor", null);

                    //8. check Duplicate Instructore Name 

                    var instructorNameExists = await context.Instructors
                              .AnyAsync(i => i.Name == instructors.Name && i.StoreId == store.Id);

                    if (instructorNameExists)
                        return ("InstructorNameAlreadyExists", null);

                    //9. Add Instructore Role 

                  



                    var Addrole = await userManager.AddToRoleAsync(user, RolesHelper.Instructor);

                    if (!Addrole.Succeeded)
                        return ("FailedToAddInstructorRole", null);
                    var roles = await userManager.GetRolesAsync(user);

                    if (!roles.Any(r => r == "Instructor"))
                        return ("UserIsNotAssignedto(Instructor_Role)", null);

                    

                    //  1. Default values
                    instructors.StoreId = store.Id;
                    instructors.UserId = UserId;
                    instructors.IsActive = true;
                    instructors.CreatedAt = DateTime.UtcNow;
                    instructors.JobTitle = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(instructors.JobTitle.ToLower().Trim());

                    // 2. Save
                    await AddAsync(instructors);
                    await SaveChangesAsync();


                    await transaction.CommitAsync();
                    return ("Success", instructors.Id);
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
