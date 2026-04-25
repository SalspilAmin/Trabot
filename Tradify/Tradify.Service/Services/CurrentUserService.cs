using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.Repositories;
using Tradify.Service.AbstractsServices;
using static Tradify.Data.AppMetaData.Router;


namespace Tradify.Service.Services
{
    public class SellerContextResult
    {
        public Sellers Seller { get; set; }
        public Stores Store { get; set; }
        public string Error { get; set; }
    }

    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ISellerService sellerService;
        private readonly ApplicationDbContext context;
        private readonly IStoreService storeService;    

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, ISellerService sellerService,
            ApplicationDbContext context , IStoreService storeService)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.sellerService = sellerService;
            this.context = context;
            this.storeService = storeService;
        }

        public int GetUserId()
        {
            var userId = httpContextAccessor.HttpContext?.User?
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User not authenticated");

            return int.Parse(userId);
        }


        public async Task<SellerContextResult> GetValidSellerContextAsync()
        {
            var userId = GetUserId();

            var seller = await sellerService.GetTableNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (seller == null)
                return new SellerContextResult { Error = "SellerNotFound" };

            if (!seller.IsActive)
                return new SellerContextResult { Error = "SellerNotActive" };

            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return new SellerContextResult { Error = "UserNotFound" };

            if (user.IsDeleted)
                return new SellerContextResult { Error = "SellerConectWithDeletedUser" };

            var store = await storeService.GetBySellerIdAsync(seller.Id);

            if (store == null)
                return new SellerContextResult { Error = "YouMustCreateStoreFirst" };

            return new SellerContextResult
            {
                Seller = seller,
                Store = store
            };
        }


    }
}
