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
using Tradify.Core.Features.Store.Queries.Models;
using Tradify.Core.Features.Store.Queries.Results;
using Tradify.Core.Features.User.Queries.Models;
using Tradify.Core.Features.User.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Core.Wrappers;
using Tradify.Data.Entities;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Core.Features.ProductVariant.Queries.Handlers
{
    public class ProductVariantQueryHandler : ResponseHandler, IRequestHandler<GetProductVariantsByProductQuery, Response<PaginatedResult<GetProductVariantByProductResponse>>>
                                                             , IRequestHandler<GetProductVariantByIdQuery, Response<GetProductVariantByIdResponse>>
                                                             , IRequestHandler<GetAllVarintByProductListQuery, List<GetProductVariantByProductResponse>>
        

    {
        #region Fields
        private readonly LocalizationService localize;
        private readonly IProductVariantService productVariantService;
        private readonly IStoreService storeService;
        private readonly IMapper mapper;
        private readonly IProductService productService;
        private readonly ICurrentUserService currentUserService;
        private readonly IFileService fileService;


        #endregion

        #region Constructor
        public ProductVariantQueryHandler(IProductVariantService productVariantService,
                                     IMapper mapper,
                                     IProductService productService,
                                     IStoreService storeService,
                                     ICurrentUserService currentUserService,
                                     LocalizationService localize,
                                     IFileService fileService) : base(localize)
        {
            this.productVariantService = productVariantService;
            this.mapper = mapper;
            this.localize = localize;
            this.storeService = storeService;
            this.productService = productService;
            this.fileService   = fileService;
            this.currentUserService = currentUserService;
        }
        #endregion

        #region Mehtods

        // Get Product Varint With Pagination With Filter
        public async Task<Response<PaginatedResult<GetProductVariantByProductResponse>>> Handle(
        GetProductVariantsByProductQuery request,
        CancellationToken cancellationToken)
        {


            //  Query
            var variants = productVariantService.GetTableNoTracking().Include(v=>v.ProductVariantImage)
                .Where(v => v.ProductId == request.ProductId);

            //  Search
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();
                variants = variants.Where(v =>
                           (v.Color != null && EF.Functions.Like(v.Color, $"%{search}%")) ||
                           (v.Size != null && EF.Functions.Like(v.Size, $"%{search}%"))
                );

            }

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


            var baseUrl = fileService.GetBaseUrl();

            foreach (var varint in result.Data)
            {
                if (varint.Image != null)
                {
                    varint.Image.MediaPath =
                        baseUrl + varint.Image.MediaPath.Replace("\\", "/");
                }
            }

            return Success(result);

        }

        // Get All Varint By Product List

        public async Task<List<GetProductVariantByProductResponse>> Handle(GetAllVarintByProductListQuery request, CancellationToken cancellationToken)
        {
            //  Query
            var variants =await productVariantService.GetTableNoTracking().Include(v => v.ProductVariantImage)
                .Where(v => v.ProductId == request.ProductId).ToListAsync(cancellationToken);




            var result = mapper.Map<List<GetProductVariantByProductResponse>>(variants);
            var baseUrl = fileService.GetBaseUrl();
            foreach (var varint in result)
            {
                if (varint.Image != null)
                {
                    varint.Image.MediaPath =
                        baseUrl + varint.Image.MediaPath.Replace("\\", "/");
                }
            }


            return result;
        }


        // Get Varint By Id 
        public async Task<Response<GetProductVariantByIdResponse>> Handle(GetProductVariantByIdQuery request, CancellationToken cancellationToken)
        {
            var variant = await productVariantService.GetTableNoTracking()
                                .Include(v => v.Product)
                                .Include(v => v.ProductVariantImage)
                                .FirstOrDefaultAsync(v => v.Id == request.Id);
            if (variant == null)
                return NotFound<GetProductVariantByIdResponse>(localize.Get("VariantNotFound"));

            var result = mapper.Map<GetProductVariantByIdResponse>(variant);
            var baseUrl = fileService.GetBaseUrl();

           
                if (result.Image != null)
                {
                result.Image.MediaPath =
                        baseUrl + result.Image.MediaPath.Replace("\\", "/");
                }
           
            return Success<GetProductVariantByIdResponse>(result);

        }

      
        #endregion

    }
}
