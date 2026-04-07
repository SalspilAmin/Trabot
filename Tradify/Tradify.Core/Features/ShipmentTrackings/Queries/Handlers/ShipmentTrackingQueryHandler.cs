using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Categorie.Queries.Models;
using Tradify.Core.Features.Categorie.Queries.Results;
using Tradify.Core.Features.Favorites.Queries.Models;
using Tradify.Core.Features.Order.Queries.Models;
using Tradify.Core.Features.Order.Queries.Results;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Core.Features.ShipmentTrackings.Queries.Models;
using Tradify.Core.Features.ShipmentTrackings.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Core.Wrappers;
using Tradify.Data.Entities;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;

namespace Tradify.Core.Features.Categorie.Queries.Handlers
{
    public class ShipmentTrackingQueryHandler : ResponseHandler,IRequestHandler<GetShipmentByOrderIdQuery, Response<GetShipmentByOrderIdResponse>>



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
        
        public async Task<Response<GetShipmentByOrderIdResponse>> Handle(GetShipmentByOrderIdQuery request,CancellationToken cancellationToken)
        {
           // var userId = currentUserService.GetUserId();

            // 1️⃣ هات الشحنة + التراكينج
            var shipment = await shipmentService.GetTableNoTracking()
                .Include(s => s.ShipmentTracking)
                .Include(s => s.Order)
                //.FirstOrDefaultAsync((s => s.OrderId == request.OrderId&& s.Order.CustomerId==userId), cancellationToken);
                .FirstOrDefaultAsync(s => s.OrderId == request.OrderId , cancellationToken);

            if (shipment == null)
                return NotFound<GetShipmentByOrderIdResponse>(localization.Get("ShipmentNotFound"));

            var tracking = shipment.ShipmentTracking;

            if (tracking == null)
                return NotFound<GetShipmentByOrderIdResponse>(localization.Get("ShipmenttrackingNotFound"));

            // 3️⃣ Mapping
            var response = mapper.Map<GetShipmentByOrderIdResponse>(tracking);

            return Success<GetShipmentByOrderIdResponse>(response);
        }

        #endregion
    }
}



