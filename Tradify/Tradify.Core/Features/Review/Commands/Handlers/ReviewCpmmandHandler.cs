using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.ProductsImages.Commands.Models;
using Tradify.Core.Features.Review.Commands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities;
using Tradify.Data.Enums;
using Tradify.Infrastructure.Repositories;
using Tradify.Service.AbstractsServices;
using Tradify.Service.AbstractsServices.AuthorizationServices;
using Tradify.Service.Services.AuthorizationServices;

namespace Tradify.Core.Features.Review.Commands.Handlers
{
    public class ReviewCpmmandHandler : ResponseHandler
        , IRequestHandler<AddReviewCommand, Response<string>>
        , IRequestHandler<UpdateReviewCommand, Response<string>>
        , IRequestHandler<DeleteReviewCommand, Response<string>>


    {

        #region Fields
        private readonly LocalizationService localize;
        private readonly IProductService productService;
        private readonly IMapper mapper;
        private readonly ICurrentUserService currentUserService;
        private readonly IReviewService reviewService;
        private readonly IOrderItemsService orderItemsService;
        private readonly IAuthorizationService authorizationService;




        #endregion

        #region Constructor
        public ReviewCpmmandHandler(
                                     IMapper mapper,
                                     IProductService productService,
                                     IReviewService reviewService,
                                     ICurrentUserService currentUserService,
                                     IOrderItemsService orderItemsService,
                                     IAuthorizationService authorizationService,

                                     LocalizationService localize) : base(localize)
        {
            this.mapper = mapper;
            this.productService = productService;
            this.localize = localize;
            this.currentUserService = currentUserService;
            this.reviewService = reviewService;
            this.orderItemsService = orderItemsService;
            this.authorizationService = authorizationService;
        }
        #endregion

        #region Methods

        public async Task<Response<string>> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            var userId = currentUserService.GetUserId();

            // 1️⃣ Check Product Exists
            var product = await productService.GetTableAsTracking().Include(p => p.Store).FirstOrDefaultAsync(p => p.Id == request.ProductId);
            if (product == null)
                return NotFound<string>(localize.Get("ProductNotFound"));

            // 2️⃣ Check Owner
            if (product.Store.SellerId == userId)
                return BadRequest<string>(localize.Get("YouCannotReviewYourOwnProduct"));

            // 3️⃣ Check Already Reviewed
            var alreadyReviewed = await reviewService
                .GetTableNoTracking()
                .AnyAsync(x => x.ProductId == request.ProductId && x.CustomerId == userId);

            if (alreadyReviewed)
                return BadRequest<string>(localize.Get("YouAlreadyReviewedThisProduct"));

            // 4️⃣ Check Verified Purchase
            var hasBought = await orderItemsService.GetTableNoTracking()
                             .Include(oi => oi.SubOrder)
                             .ThenInclude(so => so.Order)
                             .Include(oi => oi.ProductVariant)
                             .AnyAsync(oi =>
                             oi.ProductVariant.ProductId == request.ProductId &&
                             oi.SubOrder.Order.CustomerId == userId);
            // 5️⃣ Create Review
            var review = mapper.Map<Reviews>(request);
            review.CustomerId = userId;
            review.IsPurchased = hasBought;

            await reviewService.AddAsync(review);
            await reviewService.SaveChangesAsync();

            return Success(localize.Get("ReviewAddedSuccessfully"));
        }


        public async Task<Response<string>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var userId = currentUserService.GetUserId();

            // 1️⃣ Get Review
            var review = await reviewService.GetByIdAsync(request.Id);

            if (review == null)
                return NotFound<string>(localize.Get("ReviewNotFound"));

            // 2️⃣ Check Owner
            if (review.CustomerId != userId)
                return BadRequest<string>(localize.Get("YouCanOnlyEditYourReview"));

            // 3️⃣ Update
            review.Rating = (RatingValue)request.Rating;
            review.Comment = request.Comment;
            await reviewService.UpdateAsync(review);
            await reviewService.SaveChangesAsync();

            return Success(localize.Get("ReviewUpdatedSuccessfully"));

        }

            public async Task<Response<string>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
            {
            var userId = currentUserService.GetUserId();
            var isAdmin = await authorizationService.IsUserInRoleAsync(userId, "Admin");

            // 1️⃣ Get Review + Product + Store
            var review = await reviewService.GetTableAsTracking()
                .Include(r => r.Product)
                .ThenInclude(p => p.Store)
                .FirstOrDefaultAsync(r => r.Id == request.Id &&
                (r.CustomerId == userId|| r.Product.Store.SellerId == userId|| isAdmin));

            if (review == null)
                return NotFound<string>(localize.Get("ReviewNotFound"));

           

            // 3️⃣ Delete
            await reviewService.DeleteAsync(review);
            await reviewService.SaveChangesAsync();

            return Success(localize.Get("ReviewDeletedSuccessfully"));
            }

        #endregion


    }
}

