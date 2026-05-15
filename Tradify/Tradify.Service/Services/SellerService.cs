using Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Helpers;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.AbstractsServices.AuthorizationServices;
using Tradify.Service.AbstractsServices.IdentityServices;
using Tradify.Service.ServiceBases;
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Service.Services
{
    public class SellerService : Service<Sellers>, ISellerService
    {
        private readonly IGenericRepository<Sellers> sellerRepo;
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        private readonly IAuthorizationService authorizationService;            



        public SellerService(IGenericRepository<Sellers> sellerRepo, UserManager<User> userManager, ApplicationDbContext context,IAuthorizationService authorization) : base(sellerRepo)
        {
            this.sellerRepo = sellerRepo;
            this.context = context;
            this.userManager = userManager;
            this.authorizationService = authorization;


        }



        public async Task<(string, int?)> AddSellerAsync(Sellers seller ,int userId )
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    //  1. Check if user exist
                    var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

                    if (user == null)
                    {
                        return ("UserNotFound", null);
                    }

                    //2.Check if user is Dleated 
                    if (user.IsDeleted)
                        return ("UserDeleted", null) ;


                    // 3. Check is user have seller Role
                    var Addrole = await userManager.AddToRoleAsync(user, RolesHelper.Seller);
                    var roles = await userManager.GetRolesAsync(user);

                    if (!roles.Any(r => r == "Seller"))
                        return ("UserIsNotAssignedto(Seller_Role)", null);

                    //5. Cheack Busnis Name
                    var existBusinessName =await GetTableNoTracking()
                    .AnyAsync(s => s.BusinessName == seller.BusinessName);

                    if (existBusinessName)
                        return ("BusinessNameAlreadyExist", null);


                    // 4. Check is user already Seller
                    var existingSeller = await GetTableNoTracking()
                    .FirstOrDefaultAsync(s => s.UserId == userId);

                    if (existingSeller != null)
                        return ("UserIsAlreadySeller", null);
                    //  1. Default values
                    seller.UserId = userId;
                    seller.IsActive = true;
                    seller.BusinessName = seller.BusinessName.Trim().ToLower();
                    // 2. Save
                    await AddAsync(seller);
                    await SaveChangesAsync();

                    await transaction.CommitAsync();
                    return ("Success", seller.Id);
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

