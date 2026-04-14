using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Core.Features.Store.Queries.Handlers
{
    public class StoreQueryHandler : ResponseHandler , IRequestHandler<GetStoreByIdQuery, Response<GetStoreByIdResponse>>
                                                     , IRequestHandler<GetMyStoreQuery, Response<GetStoreByIdResponse>>
                                                     , IRequestHandler<GetStoresPaginationQuery, PaginatedResult<GetAllStoresResponse>>
                                                     , IRequestHandler<GetDeletedStoresQuery, PaginatedResult<GetAllStoresResponse>>
                                                     , IRequestHandler<GetAllStoreListQuery, List<GetAllStoresResponse>>


    {
        #region fields
        private readonly IMapper mapper;
        private readonly LocalizationService localization;
        private readonly IStoreService storeService;
        private readonly IFileService fileService;
        #endregion

    #region Constructor
    public StoreQueryHandler(LocalizationService localization, IFileService fileService, IMapper mapper, IStoreService storeService) : base(localization)
    {
        this.mapper = mapper;
        this.localization = localization;
        this.storeService = storeService;
            this.fileService = fileService; 
    }

        #endregion

        #region Mehtods
        // Get All Store Pagination
        public async Task<PaginatedResult<GetAllStoresResponse>> Handle(GetStoresPaginationQuery request, CancellationToken cancellationToken)
        {

            var stores = storeService.GetTableNoTracking().Include(x => x.StoreImage).AsQueryable();
            if (request.IsDeleted.HasValue)
            {
                stores = stores.IgnoreQueryFilters()
                                   .Where(v => v.IsDeleted == request.IsDeleted);
            }

            if (request.IsActive.HasValue)
            {
                stores = stores.Where(v => v.IsActive == request.IsActive);
            }

            var result = await mapper
                .ProjectTo<GetAllStoresResponse>(stores )
                .ToPaginationlist(request.PageNumber, request.PageSize);

            var baseUrl = fileService.GetBaseUrl();

            foreach (var store in result.Data)
            {
                if (store.Image != null)
                {
                    store.Image.MediaPath =
                        baseUrl + store.Image.MediaPath.Replace("\\", "/");
                }
            }
            return result;
        }


        // Get All Store List

        public async Task<List<GetAllStoresResponse>> Handle(GetAllStoreListQuery request, CancellationToken cancellationToken)
        {

                 
            var stores = await storeService.GetTableNoTracking().Include(x => x.StoreImage).ToListAsync(cancellationToken);


            var result = mapper.Map< List<GetAllStoresResponse>>(stores);
            var baseUrl = fileService.GetBaseUrl();
            foreach (var store in result)
            {
                if (store.Image != null)
                {
                    store.Image.MediaPath =
                        baseUrl + store.Image.MediaPath.Replace("\\", "/");
                }
            }
            

            return result;
        }


        //Get My Store

        public async Task<Response<GetStoreByIdResponse>> Handle(GetMyStoreQuery request, CancellationToken cancellationToken)
        {
            var store = await storeService.GetTableNoTracking()
                .Include(s => s.Seller)
                    .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(
                    s => s.SellerId == request.SellerId,
                    cancellationToken);

            if (store == null)
                return NotFound<GetStoreByIdResponse>(localization.Get("NotFound"));

            var result = mapper.Map<GetStoreByIdResponse>(store);

            return Success(result);
        }

        // Get Store By Id
        public async Task<Response<GetStoreByIdResponse>> Handle(GetStoreByIdQuery request, CancellationToken cancellationToken)
    {
            var store = await storeService.GetTableNoTracking().IgnoreQueryFilters()
                 .Include(x => x.StoreImage)
                 .Include(s => s.Seller)
                 .ThenInclude(x => x.User)
                 .FirstOrDefaultAsync(p => p.Id == request.Id);




            //var store = await storeService.GetByIdWithIncludesAsync(request.Id);

            if (store == null) return NotFound<GetStoreByIdResponse>(localization.Get("NotFound"));
            if (store .IsDeleted) return NotFound<GetStoreByIdResponse>(localization.Get("StoreISDleated"));


            var result = mapper.Map<GetStoreByIdResponse>(store);
            var baseUrl = fileService.GetBaseUrl();
            if (result.Image != null)
            {
                result.Image.MediaPath =
                    baseUrl + result.Image.MediaPath.Replace("\\", "/");
            }
           
            return Success<GetStoreByIdResponse>(result);

    }


        //GetDeletedStores
        public async Task<PaginatedResult<GetAllStoresResponse>> Handle(GetDeletedStoresQuery request, CancellationToken cancellationToken)
        {
            var deletedStores = storeService
                .GetTableIgnoreQueryFilters()
                .Where(s => s.IsDeleted);

            var result = await mapper
                .ProjectTo<GetAllStoresResponse>(deletedStores)
                .ToPaginationlist(request.PageNumber, request.PageSize);

            return result;
        }
     
        
        #endregion
    }


}

