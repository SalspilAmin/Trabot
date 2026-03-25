using AutoMapper;
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
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;

namespace Tradify.Core.Features.Store.Queries.Handlers
{
    public class StoreQueryHandler : ResponseHandler , IRequestHandler<GetStoreByIdQuery, Response<GetStoreByIdResponse>>
                                                     , IRequestHandler<GetMyStoreQuery, Response<GetStoreByIdResponse>>
                                                     , IRequestHandler<GetStoresPaginationQuery, PaginatedResult<GetStoresPaginationResponse>>
                                                     , IRequestHandler<GetDeletedStoresQuery, PaginatedResult<GetStoresPaginationResponse>>

    {
        #region fields
        private readonly IMapper mapper;
        private readonly LocalizationService localization;
        private readonly IStoreService storeService;
        #endregion

    #region Constructor
    public StoreQueryHandler(LocalizationService localization, IMapper mapper, IStoreService storeService) : base(localization)
    {
        this.mapper = mapper;
        this.localization = localization;
        this.storeService = storeService;
    }

        #endregion

        #region Mehtods
        public async Task<PaginatedResult<GetStoresPaginationResponse>> Handle(GetStoresPaginationQuery request, CancellationToken cancellationToken)
        {
            var stores = storeService.GetTableNoTracking() ;

            var result = await mapper
                .ProjectTo<GetStoresPaginationResponse>(stores )
                .ToPaginationlist(request.PageNumber, request.PageSize);

            return result;
        }
        
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
        public async Task<Response<GetStoreByIdResponse>> Handle(GetStoreByIdQuery request, CancellationToken cancellationToken)
    {
        var store = await storeService.GetByIdWithIncludesAsync(request.Id);
        if (store == null) return NotFound<GetStoreByIdResponse>(localization.Get("NotFound"));

        var result = mapper.Map<GetStoreByIdResponse>(store);

        return Success<GetStoreByIdResponse>(result);

    }
        
        public async Task<PaginatedResult<GetStoresPaginationResponse>> Handle(GetDeletedStoresQuery request, CancellationToken cancellationToken)
        {
            var deletedStores = storeService
                .GetTableIgnoreQueryFilters()
                .Where(s => s.IsDeleted);

            var result = await mapper
                .ProjectTo<GetStoresPaginationResponse>(deletedStores)
                .ToPaginationlist(request.PageNumber, request.PageSize);

            return result;
        }
     
        
        #endregion
    }


}

