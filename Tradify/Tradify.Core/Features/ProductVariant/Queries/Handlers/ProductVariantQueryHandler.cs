using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Product.Queries.Models;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Core.Features.ProductVariant.Queries.Models;
using Tradify.Core.Features.ProductVariant.Queries.Results;
using Tradify.Core.Features.User.Queries.Models;
using Tradify.Core.Features.User.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Core.Wrappers;
using Tradify.Data.Entities;
using Tradify.Service.AbstractsServices;
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Core.Features.ProductVariant.Queries.Handlers
{
    public class ProductVariantQueryHandler : ResponseHandler, IRequestHandler<GetProductVariantsByProductQuery, Response<PaginatedResult<GetProductVariantByProductResponse>>>
                                                             , IRequestHandler<GetProductVariantByIdQuery , Response<GetProductVariantByIdResponse>>
    {
        #region Fields
        private readonly LocalizationService localize;
        private readonly IProductVariantService productVariantService;
        private readonly IStoreService storeService;
        private readonly IMapper mapper;
        private readonly IProductService productService;
        private readonly ICurrentUserService currentUserService;


        #endregion

        #region Constructor
        public ProductVariantQueryHandler(IProductVariantService productVariantService,
                                     IMapper mapper,
                                     IProductService productService,
                                     IStoreService storeService,
                                     ICurrentUserService currentUserService,
                                     LocalizationService localize) : base(localize)
        {
            this.productVariantService = productVariantService;
            this.mapper = mapper;
            this.localize = localize;
            this.storeService = storeService;
            this.productService = productService;

            this.currentUserService = currentUserService;
        }
        #endregion

        #region Mehtods

        public async Task<Response<PaginatedResult<GetProductVariantByProductResponse>>> Handle(
        GetProductVariantsByProductQuery request,
        CancellationToken cancellationToken)
        {
            var currentUserId = currentUserService.GetUserId();


            //  Query
            var variants = productVariantService.GetTableNoTracking()
                .Where(v => v.ProductId == request.ProductId );

            //  Search
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();
                variants = variants.Where(v =>
                           (v.Color != null && EF.Functions.Like(v.Color, $"%{search}%")) ||
                           (v.Size != null && EF.Functions.Like(v.Size, $"%{search}%"))
                );
                
            }

           
            //  Deleted Filter
            //if (request.IsDeleted == true)
            //{
            //    variants = variants
            //        .IgnoreQueryFilters()
            //        .Where(v => v.IsDeleted);
            //}
            if (request.IsDeleted.HasValue)
            {
                variants = variants.IgnoreQueryFilters()
                                   .Where(v => v.IsDeleted == request.IsDeleted);
            }

            //  Min Price
            if (request.MinPrice.HasValue)
                variants = variants.Where(v => v.FinalPrice >= request.MinPrice);

            //  Max Price
            if (request.MaxPrice.HasValue)
                variants = variants.Where(v => v.FinalPrice <= request.MaxPrice);

            //  Discount
            if (request.Discount == true)
                variants = variants.Where(v => v.Discount > 0);

           


            // Out Of Stock
            if (request.OutOfStock == true)
                variants = variants.Where(v => v.NumberOfProductInStock == 0);

            //  Order
            variants = variants.OrderByDescending(v => v.Id);

            //  Pagination
            var result = await mapper
                .ProjectTo<GetProductVariantByProductResponse>(variants)
                .ToPaginationlist(request.PageNumber, request.PageSize);

            return Success(result);

        }
       



        public async Task<Response<GetProductVariantByIdResponse>> Handle(GetProductVariantByIdQuery request, CancellationToken cancellationToken)
        {
            var variant = await productVariantService.GetTableNoTracking()
                                .Include(v => v.Product)
                                .Include(v => v.ProductVariantImages)
                                .FirstOrDefaultAsync(v => v.Id == request.Id);
            if (variant == null)
                return NotFound<GetProductVariantByIdResponse>(localize.Get("VariantNotFound"));

            var result = mapper.Map<GetProductVariantByIdResponse>(variant);

            return Success<GetProductVariantByIdResponse>(result);

    }


        #endregion

    }
}
