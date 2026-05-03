using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Core.Features.ProductVariant.Queries.Models;
using Tradify.Core.Features.ProductVariant.Queries.Results;
using Tradify.Core.Features.Review.Queries.Models;
using Tradify.Core.Features.Review.Queries.Results;
using Tradify.Core.Features.User.Queries.Models;
using Tradify.Core.Features.User.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Core.Wrappers;
using Tradify.Data.Entities;
using Tradify.Data.Entities.Identity;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;
using Twilio.TwiML.Messaging;

namespace Tradify.Core.Features.Review.Queries.Handlers
{
    public class ReviewQueryHandler : ResponseHandler, IRequestHandler<GetProductReviewsQuery, Response<PaginatedResult<ReviewsResponse>>>
                                                     , IRequestHandler<GetInstructorReviewQuery, Response<PaginatedResult<ReviewsResponse>>>
    {
        #region Fields
        private readonly LocalizationService localize;
        private readonly IProductService productService;
        private readonly IMapper mapper;
        private readonly ICurrentUserService currentUserService;
        private readonly IReviewService reviewService;


        #endregion

        #region Constructor
        public ReviewQueryHandler(
                                      IMapper mapper,
                                      IProductService productService,
                                      IReviewService reviewService,
                                      ICurrentUserService currentUserService,

                                      LocalizationService localize) : base(localize)
        {
            this.mapper = mapper;
            this.productService = productService;
            this.localize = localize;
            this.currentUserService = currentUserService;
            this.reviewService = reviewService;
        }
        #endregion

        #region Mehtods

        // Get Product Review
        public async Task<Response<PaginatedResult<ReviewsResponse>>> Handle(GetProductReviewsQuery request, CancellationToken cancellationToken)
        {
            var currntUserId = currentUserService.GetUserId();
            var reviews =  reviewService
                .GetTableNoTracking()
                .Include(r => r.Customer)
                .Where(r => r.ProductId == request.ProductId)
                .OrderByDescending(r => r.CreatedAt);

           
            var result = await mapper.ProjectTo<ReviewsResponse>(reviews)
                .ToPaginationlist(request.PageNumber, request.PageSize);

         

            var ReviewsIds = result.Data.Select(p => p.Id).ToList();

            var IsMyReview = await reviewService
                .GetTableNoTracking()
                .Where(r => r.CustomerId == currntUserId && ReviewsIds.Contains(r.Id))
                .Select(r => r.Id)
                .ToListAsync();

            foreach (var review in result.Data)
            {
                review.IsMyReview = IsMyReview.Contains(review.Id);
            }



            return Success(result);
        }



        // Get Product Instructor
        public async Task<Response<PaginatedResult<ReviewsResponse>>> Handle(GetInstructorReviewQuery request, CancellationToken cancellationToken)
        {
            var currntUserId = currentUserService.GetUserId();
            var reviews = reviewService
                .GetTableNoTracking()
                .Include(r => r.Customer)
                .Where(r => r.InstructorId == request.InstructorId)
                .OrderByDescending(r => r.CreatedAt);


            var result = await mapper.ProjectTo<ReviewsResponse>(reviews)
                .ToPaginationlist(request.PageNumber, request.PageSize);



            var ReviewsIds = result.Data.Select(p => p.Id).ToList();

            var IsMyReview = await reviewService
                .GetTableNoTracking()
                .Where(r => r.CustomerId == currntUserId && ReviewsIds.Contains(r.Id))
                .Select(r => r.Id)
                .ToListAsync();

            foreach (var review in result.Data)
            {
                review.IsMyReview = IsMyReview.Contains(review.Id);
            }



            return Success(result);
        }

        #endregion

    }
}
