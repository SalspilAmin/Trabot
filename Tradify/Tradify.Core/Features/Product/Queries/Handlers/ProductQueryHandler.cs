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
using Tradify.Core.Features.User.Queries.Models;
using Tradify.Core.Features.User.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Core.Wrappers;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Tradify.Core.Features.Product.Queries.Handlers
{
    public class ProductQueryHandler : ResponseHandler , IRequestHandler<GetProductPaginationQuery, PaginatedResult<GetProductPaginationReponse>>
                                                       , IRequestHandler<GetProductByIdQuery, Response<GetProductByIdResponse>>
                                                      // , IRequestHandler<GetProductBySearchListQuery, List<GetProductPaginationReponse>>
                                                       , IRequestHandler<GetSellerProductsQuery, Response<PaginatedResult<GetSellerProductPaginationReponse>>>

    {
        #region fields
        private readonly IMapper mapper;
        private readonly LocalizationService localization;
        private readonly IProductService productService;
        private readonly IStoreService storeService;
        private readonly ICurrentUserService currentUserService;
        private readonly ISellerService sellerService;
        private readonly IFileService fileService;


        #endregion

        #region Constructor
                                    
        public ProductQueryHandler(LocalizationService localization,IMapper mapper,IFileService fileService , ISellerService sellerService, ICurrentUserService currentUserService, IProductService productService , IStoreService storeService) : base(localization)
        {
            this.mapper = mapper;
            this.localization = localization;
            this.productService= productService;
            this.storeService= storeService;   
            this.currentUserService = currentUserService;
            this.sellerService  = sellerService;    
            this.fileService = fileService;


        }

        #endregion

        #region Mehtods
        // Get All Product Pagition With Filter

        public async Task<PaginatedResult<GetProductPaginationReponse>> Handle(GetProductPaginationQuery request,CancellationToken cancellationToken)
        {
           

            
            var currentUserId = currentUserService.GetUserId();


            var products = productService
                .GetTableNoTracking();

            // Search
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();
                products = products.Where(p =>
                              EF.Functions.Like(p.Name, $"%{search}%") ||
                              EF.Functions.Like(p.Description, $"%{search}%"));

            }

            // Discount
            if (request.Discount == true)
            {
                products = products.Where(p =>
                    p.ProductVariants.Any(v => v.Discount > 0));
            }
            // Category
            if (request.CategoryId.HasValue)
            {
                products = products.Where(p =>
                    p.CategoryId == request.CategoryId);
            }
            //  Min Price
            if (request.MinPrice.HasValue)
            {
                products = products.Where(p =>
                    p.ProductVariants.Any(v => v.FinalPrice >= request.MinPrice));
            }

            //  Max Price
            if (request.MaxPrice.HasValue)
            {
                products = products.Where(p =>
                    p.ProductVariants.Any(v => v.FinalPrice <= request.MaxPrice));
            }
            
            // rating
            if (request.MinRating.HasValue)
            {
                var minRating = request.MinRating.Value;

                products = products.Where(p =>
                    p.Reviews.Any() &&
                    p.Reviews.Average(r => (double)r.Rating) >= minRating);
            }

            products = products.OrderByDescending(p => p.Id);

            var mappedQuery = products.ProjectTo<GetProductPaginationReponse>( mapper.ConfigurationProvider,
                              new Dictionary<string, object>
                         {
                 { "CurrentUserId", currentUserId }  });

            var result = await mappedQuery.ToPaginationlist(
                request.PageNumber,
                request.PageSize);
            return result;

         

          
        }


        //Get all Product List By Search 


        //public async Task<List<GetProductPaginationReponse>> Handle(GetProductBySearchListQuery request, CancellationToken cancellationToken)
        //{

        //    var currentUserId = currentUserService.GetUserId();


        //    var products = productService.GetTableNoTracking();

        //    // Search
        //    if (!string.IsNullOrWhiteSpace(request.Search))
        //    {
        //        var search = request.Search.Trim();
        //        products = products.Where(p =>
        //                      EF.Functions.Like(p.Name, $"%{search}%") ||
        //                      EF.Functions.Like(p.Description, $"%{search}%"));

        //    }

          

        //    products = products.OrderByDescending(p => p.Id);

        //    var result = products.ProjectTo<GetProductPaginationReponse>(mapper.ConfigurationProvider,
        //                      new Dictionary<string, object>
        //                 {
        //         { "CurrentUserId", currentUserId }  });



        //    var baseUrl = fileService.GetBaseUrl();
        //     foreach (var product in result)
        //    {
        //        if (product.MainImage != null)
        //        {
        //            product.MainImage =
        //                baseUrl + product.MainImage.MediaPath.Replace("\\", "/");
        //        }
        //    }

        //    return result;

        //}




        public async Task<Response<GetProductByIdResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = currentUserService.GetUserId();
            var productQuery = productService
                .GetTableNoTracking()
                .Where(p => p.Id == request.Id)
               
                .AsQueryable();

            var product = await productQuery
                .ProjectTo<GetProductByIdResponse>(
                    mapper.ConfigurationProvider,
                    new Dictionary<string, object>
                    {
                    { "CurrentUserId", currentUserId }
                    })
                .FirstOrDefaultAsync(cancellationToken);
            if (product == null) return NotFound<GetProductByIdResponse>(localization.Get("NotFound"));

           // var result = mapper.Map<GetProductByIdResponse>(product);

            return Success(product);

        }

        //public async Task<PaginatedResult<GetProductByCategoryResponse>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        //{
        //    var products = productService.GetProductsByCategoryAsync(request.CategoryId).OrderByDescending(p => p.Id);
        //    var result = await mapper
        //        .ProjectTo<GetProductByCategoryResponse>(products)
        //        .ToPaginationlist(request.PageNumber, request.PageSize);

        //    return result;
        //}

        public async Task<Response<PaginatedResult<GetSellerProductPaginationReponse>>> Handle(GetSellerProductsQuery request,CancellationToken cancellationToken)
        {
            var currentUserId = currentUserService.GetUserId();
            var seller = await sellerService.GetTableNoTracking().FirstOrDefaultAsync(s=>s.UserId== currentUserId);

            if (seller == null)
                return NotFound<PaginatedResult<GetSellerProductPaginationReponse>>(localization.Get("SellerNotFound"));

            // 1️⃣ هات الاستور بتاع السيلر
            var store = await storeService.GetBySellerIdAsync(seller.Id);

            if (store == null)
            return NotFound<PaginatedResult<GetSellerProductPaginationReponse>>(localization.Get("StoreNotFound"));
            // 2️⃣ هات المنتجات
            var products =  productService.GetTableNoTracking()   
                                               .Where(p => p.Store.SellerId == seller.Id);

            // Search
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();
                products = products.Where(p =>
                              EF.Functions.Like(p.Name, $"%{search}%") ||
                              EF.Functions.Like(p.Description, $"%{search}%"));

            }
            // Category
            if (request.CategoryId.HasValue)
            {
                products = products.Where(p =>
                    p.CategoryId == request.CategoryId);
            }

            // IsDeleted
            if (request.IsDeleted == true)
            {
                products = products.IgnoreQueryFilters()
                                        .Where(x => x.IsDeleted);
            }

      
          
            //  Min Price
            if (request.MinPrice.HasValue)
            {
                products = products.Where(p =>
                    p.ProductVariants.Any(v => v.FinalPrice >= request.MinPrice));
            }
            
            //  Max Price
            if (request.MaxPrice.HasValue)
            {
                products = products.Where(p =>
                    p.ProductVariants.Any(v => v.FinalPrice <= request.MaxPrice));
            }


            // Discount
            if (request.Discount == true)
            {
                products = products.Where(p =>
                    p.ProductVariants.Any(v => v.Discount > 0));
            }

            // Out Of Stock
            if (request.OutOfStock == true)
            {
                products = products.Where(p =>
                    p.ProductVariants.Any() &&
                    p.ProductVariants.All(v => v.NumberOfProductInStock == 0));
            }

          
  
            products = products.OrderByDescending(p => p.Id);


            var result = await mapper
                    .ProjectTo<GetSellerProductPaginationReponse>(products)
                    .ToPaginationlist(request.PageNumber, request.PageSize);

            return Success(result);

        }


    }

    #endregion
}

