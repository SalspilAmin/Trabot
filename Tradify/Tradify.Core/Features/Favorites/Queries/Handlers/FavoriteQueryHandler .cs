using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Favorites.Queries.Models;
using Tradify.Core.Features.Product.Queries.Models;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Core.Wrappers;
using Tradify.Data.Entities;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Core.Features.Favorites.Queries.Handlers
{
    public class FavoriteQueryHandler : ResponseHandler, IRequestHandler<GetUserFavoritesQuery, PaginatedResult<GetSellerProductPaginationReponse>>

    {
        #region fields
        private readonly IMapper mapper;
        private readonly LocalizationService localization;
        private readonly IProductService productService;
        private readonly IStoreService storeService;
        private readonly ICurrentUserService currentUserService;
        private readonly IFavoriteService favoriteService;



        #endregion

        #region Constructor

        public FavoriteQueryHandler(LocalizationService localization,
            IFavoriteService favoriteService,IMapper mapper, ICurrentUserService currentUserService, IProductService productService, IStoreService storeService) : base(localization)
        {
            this.mapper = mapper;
            this.localization = localization;
            this.productService = productService;
            this.storeService = storeService;
            this.currentUserService = currentUserService;
            this.favoriteService   = favoriteService;   

        }

        #endregion

        #region Mehtods
        public async Task<PaginatedResult<GetSellerProductPaginationReponse>> Handle(
           GetUserFavoritesQuery request,
           CancellationToken cancellationToken)
        {
            var userId = currentUserService.GetUserId();

            var productsfavorit = favoriteService.GetTableNoTracking()
                .Where(f => f.UserId == userId)
                .Select(f => f.Product) // 🔥 أهم خطوة
                .AsQueryable();
            productsfavorit = productsfavorit.OrderByDescending(p => p.Id);



            var result = await mapper
                    .ProjectTo<GetSellerProductPaginationReponse>(productsfavorit)
                    .ToPaginationlist(request.PageNumber, request.PageSize);

            return result;
            
        }
        #endregion
    }
}
