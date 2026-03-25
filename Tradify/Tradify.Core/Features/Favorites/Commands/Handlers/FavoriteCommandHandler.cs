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
                                         IRequestHandler<AddFavoriteCommand, Response<string>>,
                                         IRequestHandler<DeleteFavoriteCommand, Response<string>>,
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
        public async Task<Response<string>> Handle(AddFavoriteCommand request, CancellationToken cancellationToken)
        {
            var userId = currentUserService.GetUserId();
            // ✅ Check if already exists
            var exists = await favoriteService.GetTableNoTracking()
                .AnyAsync(x => x.UserId == userId && x.ProductId == request.ProductId);

            if (exists)
                return BadRequest<string>(localize.Get("AlreadyAddedToFavorites"));

            // ✅ Mapping
            var favorite = mapper.Map<Favorite>(request);

            // ✅ Add
            await favoriteService.AddAsync(favorite);
            await favoriteService.SaveChangesAsync();     

            return Success(localize.Get("AddedToFavoritesSuccessfully"));
        }

        public async Task<Response<string>> Handle(DeleteFavoriteCommand request, CancellationToken cancellationToken)
        {
            var userId = currentUserService.GetUserId();

            // ✅ Get favorite
            var favorite = await favoriteService.GetTableAsTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == userId);

            if (favorite == null)
                return NotFound<string>(localize.Get("FavoriteNotFound"));


            // ✅ Delete
            await  favoriteService.DeleteAsync(favorite);
            await favoriteService.SaveChangesAsync();

            return Success(localize.Get("RemovedFromFavoritesSuccessfully"));
        }
        public async Task<Response<ToggleFavoriteResponse>> Handle(ToggleFavoriteCommand request, CancellationToken cancellationToken)
        {
            var userId = currentUserService.GetUserId();

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
