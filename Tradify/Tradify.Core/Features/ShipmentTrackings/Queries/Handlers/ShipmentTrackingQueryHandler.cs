using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Categorie.Queries.Models;
using Tradify.Core.Features.Categorie.Queries.Results;
using Tradify.Core.Features.Education.Queries.Models;
using Tradify.Core.Features.Education.Queries.Results;
using Tradify.Core.Features.Favorites.Queries.Models;
using Tradify.Core.Features.Order.Queries.Models;
using Tradify.Core.Features.Order.Queries.Results;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Core.Features.ShipmentTrackings.Queries.Models;
using Tradify.Core.Features.ShipmentTrackings.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Core.Wrappers;
using Tradify.Data.Entities;
using Tradify.Data.Enums;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Core.Features.Categorie.Queries.Handlers
{
    public class ShipmentTrackingQueryHandler : ResponseHandler
                                              ,IRequestHandler<GetShipmentByOrderIdQuery, Response<GetShipmentByOrderIdResponse>>
                                              ,IRequestHandler<GetSellerShipmentQuery, Response<PaginatedResult<GetSellerShipmentResponse>>>
                                              , IRequestHandler<GetShipmentTrackingByShipmentQuery, List<GetShipmentTrackingByShipmentResponse>>




    {
        #region fields
        private readonly IMapper mapper;
        private readonly LocalizationService localization;
        private readonly ICurrentUserService currentUserService;
        private readonly IShipmentService shipmentService;
        private readonly IOrdersService ordersService;
        private readonly IShipmentTrackingService shipmentTrackingService;




        #endregion

        #region Constructor

        public ShipmentTrackingQueryHandler(LocalizationService localization,
             IMapper mapper, ICurrentUserService currentUserService
            , IOrdersService ordersService, IShipmentService shipmentService,
             IShipmentTrackingService shipmentTrackingService) : base(localization)
        {
            this.mapper = mapper;
            this.localization = localization;
            this.currentUserService = currentUserService;
            this.ordersService = ordersService;
            this.shipmentTrackingService = shipmentTrackingService;
            this.shipmentService = shipmentService;

        }

        #endregion

        #region Mehtods

        public async Task<Response<GetShipmentByOrderIdResponse>> Handle(GetShipmentByOrderIdQuery request, CancellationToken cancellationToken)
        {

            var shipment = await shipmentService.GetTableNoTracking()
                .Include(s => s.ShipmentTrackings)
                .FirstOrDefaultAsync(s => s.SubOrderId == request.SubOrderId);

            Console.WriteLine($"Request Id = {request.SubOrderId}");
            Console.WriteLine($"Shipment Found = {shipment != null}");
            
            if (shipment == null)
                return NotFound<GetShipmentByOrderIdResponse>(localization.Get("ShipmentNotFound"));

            
            // 3️⃣ Mapping
            var response = mapper.Map<GetShipmentByOrderIdResponse>(shipment);

            return Success<GetShipmentByOrderIdResponse>(response);
        }


        public async Task<Response<PaginatedResult<GetSellerShipmentResponse>>> Handle(GetSellerShipmentQuery request, CancellationToken cancellationToken)
        {
            var ValidSeller = await currentUserService.GetValidSellerContextAsync();

            if (ValidSeller.Error != null)
                return BadRequest<PaginatedResult<GetSellerShipmentResponse>>(localization.Get(ValidSeller.Error));



            // 2. Get Seller , Store
            var seller = ValidSeller.Seller;
            var store = ValidSeller.Store;

            //3. Cheack If Store Type Is Product 

            if (store.Type != StoreType.Product)
                return BadRequest<PaginatedResult<GetSellerShipmentResponse>>(localization.Get("ThisActionAllowedForProductStoresOnly"));

            var shipments =  shipmentService.GetTableNoTracking()
                .Include(s=>s.SubOrder)
                .Where(s => s.SubOrder.StoreId == store.Id)
                .OrderByDescending(o => o.CreatedAt).AsQueryable();


            // 2️⃣ Pagination

            var result = await mapper.ProjectTo<GetSellerShipmentResponse>(shipments)
                .ToPaginationlist(request.PageNumber, request.PageSize);

            return Success(result);
            
        }



        public async Task<List<GetShipmentTrackingByShipmentResponse>> Handle(GetShipmentTrackingByShipmentQuery request, CancellationToken cancellationToken)
        {


            var shipmentTracking = await shipmentTrackingService.GetTableNoTracking()
                .Where(e => e.ShipmentId == request.ShipmentId).ToListAsync();



            var result = mapper.Map<List<GetShipmentTrackingByShipmentResponse>>(shipmentTracking);

            return result;
        }

        #endregion
    }
}



