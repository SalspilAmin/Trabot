using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Enums;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;
using static Tradify.Data.AppMetaData.Router;
namespace Tradify.Service.Services
{
    public class ReviewService : Service<Reviews>, IReviewService
    {
        private readonly ICurrentUserService currentUserService;
        private readonly ApplicationDbContext context;
        private readonly IInstructorsService instructorsService;
        private readonly IProductService productService;
        private readonly IOrderItemsService orderItemsService;
        private readonly IBookingsService bookingsService;
        private readonly ILogger<ReviewService> logger;

        public ReviewService(IGenericRepository<Reviews> repository , IProductService productService 
            , ICurrentUserService currentUserService
            , ApplicationDbContext context , IInstructorsService instructorsService
            , IOrderItemsService orderItemsService ,IBookingsService bookingsService
            , ILogger<ReviewService> logger) : base(repository)
        {
            this.productService = productService;
            this.currentUserService = currentUserService;
            this.context = context;
            this.instructorsService = instructorsService; 
            this.orderItemsService = orderItemsService; 
            this.bookingsService = bookingsService; 
            this.logger = logger;
        }


        public async Task<(string, int?)> AddReview(Reviews reviews)
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
                        return ("UserNotHavePermissionToReview", null);

                    if (reviews.ProductId.HasValue && reviews.InstructorId.HasValue)
                        return ("ChooseOnlyOneTarget", null);

                    if (!reviews.ProductId.HasValue && !reviews.InstructorId.HasValue)
                        return ("TargetRequired", null);


                    if (reviews.ProductId.HasValue)
                    {
                        // 1️. Check Product Exists
                        var product = await productService.GetTableAsTracking().Include(p => p.Store).ThenInclude(s=>s.Seller)
                            .FirstOrDefaultAsync(p => p.Id == reviews.ProductId);
                        if (product == null) return ("ProductNotFound", null);

                        // 2️. Check Owner
                        if (product.Store.Seller.UserId == curntUserId)
                            return("YouCannotReviewYourOwnProduct",null);

                        // 3️. Check Already Reviewed
                        var alreadyReviewed = await GetTableNoTracking()
                            .AnyAsync(x => x.ProductId == reviews.ProductId && x.CustomerId == curntUserId);

                        if (alreadyReviewed)
                            return ("YouAlreadyReviewedThisProduct",null);

                        // 4️. Check Verified Purchase

                        var hasBought = await orderItemsService.GetTableNoTracking()
                                         .Include(oi => oi.SubOrder)
                                         .ThenInclude(so => so.Order)
                                         .Include(oi => oi.ProductVariant)
                                         .AnyAsync(oi =>
                                         oi.ProductVariant.ProductId == reviews.ProductId &&
                                         oi.SubOrder.Order.OrderStatus == OrderStatus.delivered &&
                                         oi.SubOrder.Order.CustomerId == curntUserId);
                        //  1. Default values
                        reviews.ProductId = product.Id;
                        reviews.InstructorId = null;
                        reviews.IsPurchased = hasBought;
                        
                    }

                    if (reviews.InstructorId.HasValue)
                    {
                        // 1️. Check Product Exists
                        var instructor = await instructorsService.GetTableAsTracking().Include(p => p.Store).ThenInclude(s => s.Seller)
                            .FirstOrDefaultAsync(p => p.Id == reviews.InstructorId);
                        if (instructor == null) return ("InstructorNotFound", null);

                        // 2️. Check Owner
                        if (instructor.Store.Seller.UserId == curntUserId)
                            return ("YouCannotReviewYourOwnInstructor", null);

                        if (instructor.UserId == curntUserId)
                            return ("YouCantReviewYourSelf",null);

                        // 3️. Check Already Reviewed
                        var alreadyReviewed = await GetTableNoTracking()
                            .AnyAsync(x => x.InstructorId == reviews.InstructorId && x.CustomerId == curntUserId);

                        if (alreadyReviewed)
                            return ("YouAlreadyReviewedThisInstructor", null);

                        // 4️. Check Verified Purchase

                        var hasBooking = await bookingsService.GetTableNoTracking()
                                         .Include(b => b.Schedule)
                                         .AnyAsync(b =>
                                         b.Schedule.InstructorId == reviews.InstructorId &&
                                         b.CustomerId == curntUserId);

                      

                        //  1. Default values
                        reviews.ProductId = null;
                        reviews.InstructorId = instructor.Id;
                        reviews.IsPurchased = hasBooking;
                      
                    }

                    if (string.IsNullOrWhiteSpace(reviews.Comment))
                        reviews.Comment = null;
                    //  2. Default values

                    reviews.CustomerId = curntUserId;
                    reviews.CreatedAt = DateTime.UtcNow;

                    // 3. Save
                    await AddAsync(reviews);
                    await SaveChangesAsync();


                    await transaction.CommitAsync();
                    return ("Success", reviews.Id);
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
