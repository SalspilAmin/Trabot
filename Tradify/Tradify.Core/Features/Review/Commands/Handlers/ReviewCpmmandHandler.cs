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
        , IRequestHandler<AddProductReviewCommand, Response<string>>
        , IRequestHandler<AddInstructorReviewCommand, Response<string>>
        , IRequestHandler<UpdateReviewCommand, Response<string>>
        , IRequestHandler<DeleteReviewCommand, Response<string>>


    {

        #region Fields
        private readonly LocalizationService localize;
        private readonly IMapper mapper;
        private readonly IReviewService reviewService;
        private readonly ICurrentUserService currentUserService;
        private readonly IAuthorizationService authorizationService;

        #endregion

        #region Constructor
        public ReviewCpmmandHandler(
                                     IMapper mapper,
                                     IProductService productService,
                                     IReviewService reviewService,
                                    ICurrentUserService currentUserService,
                                    IAuthorizationService authorizationService,
                                     LocalizationService localize) : base(localize)
        {
            this.mapper = mapper;
            this.localize = localize;
            this.reviewService = reviewService;
            this.currentUserService = currentUserService;   
            this.authorizationService = authorizationService;   
        }
        #endregion

        #region Methods

        // Add Product Review
        public async Task<Response<string>> Handle(AddProductReviewCommand request, CancellationToken cancellationToken)
        {
            var review = mapper.Map<Data.Entities.Reviews>(request);


            var result = await reviewService.AddReview(review);

            if (result.Item1 != "Success")
            {
                return BadRequest<string>(localize.Get(result.Item1));
            }
            else
            {
                return Success<string>("Success", meta: result.Item2);
            }


        }




        // Add Product Review
        public async Task<Response<string>> Handle(AddInstructorReviewCommand request, CancellationToken cancellationToken)
        {
            var review = mapper.Map<Data.Entities.Reviews>(request);


            var result = await reviewService.AddReview(review);

            if (result.Item1 != "Success")
            {
                return BadRequest<string>(localize.Get(result.Item1));
            }
            else
            {
                return Success<string>("Success", meta: result.Item2);
            }


        }

        // Update User Review
        public async Task<Response<string>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var userId = currentUserService.GetUserId();

            // 1️⃣ Get Review
            var review = await reviewService.GetTableAsTracking()
                .FirstOrDefaultAsync(r=>r.Id==request.Id&&r.CustomerId== userId);

            if (review == null)
                return NotFound<string>(localize.Get("ReviewNotFound"));

          
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

            var review = await reviewService.GetTableAsTracking()
                .FirstOrDefaultAsync(r => r.Id == request.Id);

            if (review == null)
                return NotFound<string>(localize.Get("ReviewNotFound"));

            var isOwner = review.CustomerId == userId;

            var isProductSeller =
                review.Product != null &&
                review.Product.Store.Seller.UserId == userId;

            var isInstructorOwner =
                review.Instructor != null &&
                review.Instructor.UserId == userId;

            if (!(isOwner || isProductSeller || isInstructorOwner || isAdmin))
                return Unauthorized<string>(localize.Get("Unauthorized"));

            await reviewService.DeleteAsync(review);
            await reviewService.SaveChangesAsync();

            return Success(localize.Get("ReviewDeletedSuccessfully"));
        }
        #endregion


    }
}

