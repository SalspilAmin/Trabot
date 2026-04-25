using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Favorites.Commands.Models;
using Tradify.Core.Features.Favorites.Queries.Results;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;

namespace Tradify.Core.Features.Favorites.Commands.Handlers
{
    public class FavoriteCommandHandler : ResponseHandler,
                                         IRequestHandler<ToggleFavoriteCommand, Response<ToggleFavoriteResponse>>










    {
        #region Fields
        private readonly LocalizationService localize;
        private readonly IProductService productService;
        private readonly IMapper mapper;
        private readonly IFavoriteService favoriteService;
        private readonly ICurrentUserService currentUserService;


        #endregion

        #region Constructor
        public FavoriteCommandHandler(IProductService productService,
                                     IMapper mapper,
                                     IFavoriteService favoriteService,
                                     ICurrentUserService currentUserService,
                                     LocalizationService localize) : base(localize)
        {
            this.productService = productService;
            this.mapper = mapper;
            this.localize = localize;
            this.currentUserService = currentUserService;
            this.favoriteService=favoriteService;
        }
        #endregion

        #region Methods
        
        public async Task<Response<ToggleFavoriteResponse>> Handle(ToggleFavoriteCommand request, CancellationToken cancellationToken)
        {
            var userId = currentUserService.GetUserId();

            var product = await productService.GetByIdAsync(request.ProductId);

            if (product == null)
                return NotFound<ToggleFavoriteResponse>(localize.Get("ProductNotFound"));

            var favorite = await favoriteService.GetTableAsTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == request.ProductId);


            if (favorite != null)
            {
                await favoriteService.DeleteAsync(favorite);
                await favoriteService.SaveChangesAsync();

                return Success(new ToggleFavoriteResponse
                {
                    IsFavorite = false
                });
            }

            var newFavorite = new Favorite
            {
                UserId = userId,
                ProductId = request.ProductId
            };

            await favoriteService.AddAsync(newFavorite);
            await favoriteService.SaveChangesAsync();

            return Success(new ToggleFavoriteResponse
            {
                IsFavorite = true
            });
        }
        
        #endregion
    } 
}
