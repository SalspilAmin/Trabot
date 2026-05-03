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
using Tradify.Core.Features.Store.Queries.Models;
using Tradify.Core.Features.Store.Queries.Results;
using Tradify.Core.Features.User.Queries.Models;
using Tradify.Core.Features.User.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Core.Wrappers;
using Tradify.Data.Entities;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Core.Features.Product.Queries.Handlers
{
    public class ProductQueryHandler : ResponseHandler , IRequestHandler<GetProductPaginationQuery, Response<PaginatedResult<GetProductPaginationReponse>>>
                                                       , IRequestHandler<GetProductByIdQuery, Response<GetProductByIdResponse>>
                                                       , IRequestHandler<GetProductBySearchListQuery, List<GetProductPaginationReponse>>
                                                       , IRequestHandler<GetSellerProductsQuery, Response<PaginatedResult<GetSellerProductPaginationReponse>>>
                                                       , IRequestHandler<GetAllProductListQuery, List<GetProductPaginationReponse>>
                                                       , IRequestHandler<GetProductByStoreQuery, List<GetProductPaginationReponse>>
                                                       , IRequestHandler<GetProductDiscountQuery, PaginatedResult<GetProductDiscountResponse>>
        


    {
        #region fields
        private readonly IMapper mapper;
        private readonly LocalizationService localization;
        private readonly IProductService productService;
        private readonly IStoreService storeService;
        private readonly ICurrentUserService currentUserService;
        private readonly ISellerService sellerService;
        private readonly IFileService fileService;
        private readonly UserManager<Tradify.Data.Entities.Identity.User> userManager;
        private readonly IFavoriteService favoriteService;


        #endregion

        #region Constructor

        public ProductQueryHandler(LocalizationService localization,IMapper mapper,IFileService fileService 
            , ISellerService sellerService, ICurrentUserService currentUserService,
            IProductService productService , IStoreService storeService
            , UserManager<Tradify.Data.Entities.Identity.User> userManager,
            IFavoriteService favoriteService) : base(localization)
        {
            this.mapper = mapper;
            this.localization = localization;
            this.productService= productService;
            this.storeService= storeService;   
            this.currentUserService = currentUserService;
            this.sellerService  = sellerService;    
            this.fileService = fileService;
            this.userManager = userManager;
            this.favoriteService = favoriteService;

        }

        #endregion

        #region Mehtods
        // Get All Product Pagition With Filter

        #region Get All Product Pagition With Filter
        public async Task<Response<PaginatedResult<GetProductPaginationReponse>>> Handle(GetProductPaginationQuery request, CancellationToken cancellationToken)
        {



              var currentUserId = currentUserService.GetUserId();
           // var currentUserId = request.UserId;

            var products = productService
                .GetTableNoTracking().Include(p => p.ProductImages).Include(p => p.Reviews)
                .Include(p => p.Category).Include(p => p.Favorites).AsQueryable();


            // Store
            if (request.StoreId.HasValue)
            {
                var store = await storeService
                                   .GetTableNoTracking()
                                   .Where(s => s.Id == request.StoreId)
                                   .Select(s => new { s.Id, s.Type })
                                   .FirstOrDefaultAsync();
                if (store == null)
                    return BadRequest<PaginatedResult<GetProductPaginationReponse>>(localization.Get("StoreNotFound"));

                if (store.Type != Data.Enums.StoreType.Product)
                    return BadRequest<PaginatedResult<GetProductPaginationReponse>>(localization.Get("ThisStoreTypeDosn'tSupportProducts"));


                products = products.Where(p =>
                    p.StoreId == request.StoreId);
            }


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



            var result = await mapper
   .ProjectTo<GetProductPaginationReponse>(products)
   .ToPaginationlist(request.PageNumber, request.PageSize);

            var productIds = result.Data.Select(p => p.Id).ToList();

            var favorites = await favoriteService
                .GetTableNoTracking()
                .Where(f => f.UserId == currentUserId && productIds.Contains(f.ProductId))
                .Select(f => f.ProductId)
                .ToListAsync();

            foreach (var product in result.Data)
            {
                product.IsFavorite = favorites.Contains(product.Id);
            }



            return Success(result);

        }
        #endregion

        // Get All Product List

        #region Get All Product List



        public async Task<List<GetProductPaginationReponse>> Handle(GetAllProductListQuery request, CancellationToken cancellationToken)
        {
             //var currentUserId = request.UserId;
            var currentUserId = currentUserService.GetUserId();

            var query = productService.GetTableNoTracking().Include(x => x.ProductImages)
               .Include(p => p.Reviews).Include(p => p.Favorites).AsQueryable();

          

            // Search
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();
                query = query.Where(p =>
                              EF.Functions.Like(p.Name, $"%{search}%") ||
                              EF.Functions.Like(p.Description, $"%{search}%"));

            }

            // Store
            if (request.StoreId.HasValue)
            {
                query = query.Where(p =>
                    p.StoreId == request.StoreId);
            }

            // Discount
            if (request.Discount == true)
            {
                query = query.Where(p =>
                    p.ProductVariants.Any(v => v.Discount > 0));
            }
            // Category
            if (request.CategoryId.HasValue)
            {
                query = query.Where(p =>
                    p.CategoryId == request.CategoryId);
            }
            //  Min Price
            if (request.MinPrice.HasValue)
            {
                query = query.Where(p =>
                    p.ProductVariants.Any(v => v.FinalPrice >= request.MinPrice));
            }

            //  Max Price
            if (request.MaxPrice.HasValue)
            {
                query = query.Where(p =>
                    p.ProductVariants.Any(v => v.FinalPrice <= request.MaxPrice));
            }

            // rating
            if (request.MinRating.HasValue)
            {
                var minRating = request.MinRating.Value;

                query = query.Where(p =>
                    p.Reviews.Any() &&
                    p.Reviews.Average(r => (double)r.Rating) >= minRating);
            }

            query = query.OrderByDescending(p => p.Id);

            var products = await query.ToListAsync(cancellationToken);


            var result = mapper.Map<List<GetProductPaginationReponse>>(products);

            var productIds = result.Select(p => p.Id).ToList();

            var favorites = await favoriteService
                .GetTableNoTracking()
                .Where(f => f.UserId == currentUserId && productIds.Contains(f.ProductId))
                .Select(f => f.ProductId)
                .ToListAsync();

            foreach (var product in result)
            {
                product.IsFavorite = favorites.Contains(product.Id);
            }

            //var baseUrl = fileService.GetBaseUrl();

            //foreach (var product in result)
            //{
            //    if (product.MainImage != null)
            //    {
            //        product.MainImage.MediaPath =
            //            baseUrl + product.MainImage.MediaPath.Replace("\\", "/");
            //    }
            //}

            return result;


        }

        #endregion

        //Get all Product List By Search 

        #region  Get all Product List By Search 


        public async Task<List<GetProductPaginationReponse>> Handle(GetProductBySearchListQuery request, CancellationToken cancellationToken)
        {

           //  var currentUserId = request.UserId;
            var currentUserId = currentUserService.GetUserId();

            var query = productService.GetTableNoTracking().Include(x => x.ProductImages)
                .Include(p => p.Reviews).Include(p => p.Favorites).AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();
                query = query.Where(p =>
                              EF.Functions.Like(p.Name, $"%{search}%") ||
                              EF.Functions.Like(p.Description, $"%{search}%"));

            }
            var products = await query.ToListAsync(cancellationToken);

            var result = mapper.Map<List<GetProductPaginationReponse>>(products);

            var productIds = result.Select(p => p.Id).ToList();

            var favorites = await favoriteService
                .GetTableNoTracking()
                .Where(f => f.UserId == currentUserId && productIds.Contains(f.ProductId))
                .Select(f => f.ProductId)
                .ToListAsync();

            foreach (var product in result)
            {
                product.IsFavorite = favorites.Contains(product.Id);
            }

            //var baseUrl = fileService.GetBaseUrl();

            //foreach (var product in result)
            //{
            //    if (product.MainImage != null)
            //    {
            //        product.MainImage.MediaPath =
            //            baseUrl + product.MainImage.MediaPath.Replace("\\", "/");
            //    }
            //}

            return result;

        }

        #endregion

        // Get Product By Discount And Get Discount With It 

        #region Get Product By Discount
        public async Task<PaginatedResult<GetProductDiscountResponse>> Handle(GetProductDiscountQuery request, CancellationToken cancellationToken)
        {

            var products = productService
                .GetTableNoTracking().
                Include(p=>p.ProductVariants)
                .Where(p =>p.ProductVariants.Any(v => v.Discount > 0));

            var result = await mapper
                                .ProjectTo<GetProductDiscountResponse>(products)
                                .ToPaginationlist(request.PageNumber, request.PageSize);

            return result;

        }

        #endregion


        //Get all Product List By Store 

        #region Get all Product List By Store 

        public async Task<List<GetProductPaginationReponse>> Handle(GetProductByStoreQuery request, CancellationToken cancellationToken)
        {

            // var currentUserId = request.UserId;
            var currentUserId = currentUserService.GetUserId();

            var query = productService.GetTableNoTracking().Include(x => x.ProductImages)
                .Include(p => p.Reviews).Include(p => p.Favorites).AsQueryable();

            // Store
            if (request.StoreId > 0)
            {
                query = query.Where(p =>
                    p.StoreId == request.StoreId);
            }

            var products = await query.ToListAsync(cancellationToken);

            var result = mapper.Map<List<GetProductPaginationReponse>>(products);

            var productIds = result.Select(p => p.Id).ToList();

            var favorites = await favoriteService
                .GetTableNoTracking()
                .Where(f => f.UserId == currentUserId && productIds.Contains(f.ProductId))
                .Select(f => f.ProductId)
                .ToListAsync();

            foreach (var product in result)
            {
                product.IsFavorite = favorites.Contains(product.Id);
            }

            //var baseUrl = fileService.GetBaseUrl();

            //foreach (var product in result)
            //{
            //    if (product.MainImage != null)
            //    {
            //        product.MainImage.MediaPath =
            //            baseUrl + product.MainImage.MediaPath.Replace("\\", "/");
            //    }
            //}

            return result;

        }


        #endregion


        // Get Product By Id 

        #region Get Product By Id 
        public async Task<Response<GetProductByIdResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
             var currentUserId = currentUserService.GetUserId();
           //var currentUserId = request.UserId;

            var userExist = await userManager.FindByIdAsync(currentUserId.ToString());
            if (userExist==null)
                return NotFound<GetProductByIdResponse>(localization.Get("UserNotFound"));
            var product = await productService
                .GetTableNoTracking().Include(p=>p.ProductImages).Include(p=>p.Reviews)
                .Include(p => p.Category).Include(p => p.Favorites)
    
                .FirstOrDefaultAsync(p => p.Id == request.Id) ;

            
            if (product == null) return NotFound<GetProductByIdResponse>(localization.Get("ProductNotFound"));

            var result = mapper.Map<GetProductByIdResponse>(product);

            //var baseUrl = fileService.GetBaseUrl();
            //if (result.Images != null)
            //{
            //    foreach (var image in result.Images ?? [])
            //    {
            //        image.MediaPath =
            //            baseUrl + image.MediaPath.Replace("\\", "/");
            //    }
            //}
            result.IsFavorite = product.Favorites
                  .Any(f => f.UserId == currentUserId);
            return Success<GetProductByIdResponse>(result);

        }

        #endregion

        // Get Seller Products

        #region Get Seller Products


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

        #endregion
    }

    #endregion
}

