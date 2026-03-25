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

namespace Tradify.Core.Features.Review.Queries.Handlers
{
    public class ReviewQueryHandler : ResponseHandler, IRequestHandler<GetProductReviewsQuery, Response<PaginatedResult<ProductReviewsResponse>>>
    {
        #region Fields
        private readonly LocalizationService localize;
        private readonly IProductService productService;
        private readonly IMapper mapper;
        private readonly ICurrentUserService currentUserService;
        private readonly IReviewService reviewService;
        private readonly IOrderItemsService orderItemsService;


        #endregion

        #region Constructor
        public ReviewQueryHandler(
                                      IMapper mapper,
                                      IProductService productService,
                                      IReviewService reviewService,
                                      ICurrentUserService currentUserService,
                                      IOrderItemsService orderItemsService,

                                      LocalizationService localize) : base(localize)
        {
            this.mapper = mapper;
            this.productService = productService;
            this.localize = localize;
            this.currentUserService = currentUserService;
            this.reviewService = reviewService;
            this.orderItemsService = orderItemsService;
        }
        #endregion

        #region Mehtods

        public async Task<Response<PaginatedResult<ProductReviewsResponse>>> Handle(GetProductReviewsQuery request, CancellationToken cancellationToken)
        {
            var reviews =  reviewService
                .GetTableNoTracking()
                .Include(r => r.Customer)
                .Where(r => r.ProductId == request.ProductId)
                .OrderByDescending(r => r.CreatedAt);

           
            var result = await mapper.ProjectTo<ProductReviewsResponse>(reviews)
                .ToPaginationlist(request.PageNumber, request.PageSize);

            return Success(result);
        }
      
        #endregion

    }
}
