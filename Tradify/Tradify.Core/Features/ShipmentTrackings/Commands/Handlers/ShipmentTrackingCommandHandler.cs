using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Categorie.Commands.Models;
using Tradify.Core.Features.Categorie.Queries.Models;
using Tradify.Core.Features.Categorie.Queries.Results;
using Tradify.Core.Features.ProductVariant.Commands.Models;
using Tradify.Core.Features.ShipmentTrackings.Commands.Models;
using Tradify.Core.Features.Store.Commands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities;
using Tradify.Data.Enums;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;

namespace Tradify.Core.Features.ShipmentTrackings.Commands.Handlers
{
    public class ShipmentTrackingCommandHandler : ResponseHandler, IRequestHandler<CreateShipmentCommand, Response<string>>
                                                                 , IRequestHandler<UpdateShipmentStatusCommand, Response<string>>





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

        public ShipmentTrackingCommandHandler(LocalizationService localization,
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
        

        public async Task<Response<string>> Handle(CreateShipmentCommand request, CancellationToken cancellationToken)
        {
            var userId = currentUserService.GetUserId();

            // 1️⃣ Get Order
            var order = await ordersService.GetTableAsTracking()
                
                .FirstOrDefaultAsync(o => o.Id == request.OrderId);

            if (order == null)
                return NotFound<string>(localization.Get("OrderNotFound"));

          

            // 3️⃣ Check Payment
            if (order.PaymentStatus != PaymentStatus.Paid)
                return BadRequest<string>(localization.Get("OrderNotPaid"));

            // 4️⃣ Check already shipped
            var alreadyShipped = await shipmentService.GetTableNoTracking()
                .AnyAsync(s => s.OrderId == order.Id);

            if (alreadyShipped)
                return BadRequest<string>(localization.Get("AlreadyShipped"));

            // 5️⃣ Create Tracking
            var tracking = new ShipmentTracking
            {
                //OrderId = order.Id,
                TrackingNumber = GenerateTrackingNumber(),
                ShipmentStatus = ShipmentStatus.Pending,
                UpdatedAt = DateTime.UtcNow
            };

            await shipmentTrackingService.AddAsync(tracking);
            await shipmentTrackingService.SaveChangesAsync();

            // 6️⃣ Create Shipment
            var shipment = new Shipments
            {
                OrderId = order.Id,
                ShipmentTrackingId = tracking.Id,
                UpdatedAT = DateTime.UtcNow
            };

            await shipmentService.AddAsync(shipment);
            await shipmentService.SaveChangesAsync();

            return Success(localization.Get("ShipmentCreatedSuccessfully"));
        }

        private string GenerateTrackingNumber()
        {
            return $"TRK-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}"; 
        }

        public async Task<Response<string>> Handle(UpdateShipmentStatusCommand request, CancellationToken cancellationToken)
        {
            // 1️⃣ Get Tracking

            var shipmenttracking = await shipmentTrackingService.GetTableAsTracking()
                .FirstOrDefaultAsync(s => s.Id == request.ShipmentTrackingId, cancellationToken);

            if (shipmenttracking == null)
                return NotFound<string>(localization.Get("ShipmenttrackingNotFound"));

            var shipment = await shipmentService.GetTableNoTracking()
                            .FirstOrDefaultAsync(s => s.ShipmentTrackingId == shipmenttracking.Id);

            if (shipment == null)
                return NotFound<string>(localization.Get("ShipmentNotFound"));


            // 2️⃣ منع نفس الحالة
            if (shipmenttracking.ShipmentStatus == request.Status)
                return BadRequest<string>(localization.Get("StatusAlreadySet"));

            // 3️⃣ منع التعديل بعد النهاية
            if (shipmenttracking.ShipmentStatus == ShipmentStatus.Delivered ||
                shipmenttracking.ShipmentStatus == ShipmentStatus.Returned)
            {
                return BadRequest<string>(localization.Get("CannotUpdateFinalStatus"));
            }

            shipmenttracking.ShipmentStatus = request.Status;
            shipmenttracking.UpdatedAt = DateTime.UtcNow;

          
            // 1️⃣ Get Order
            var order = await ordersService.GetTableAsTracking()
               
                .FirstOrDefaultAsync(o => o.Id ==  shipment.OrderId);

            if (order == null)
                return NotFound<string>(localization.Get("OrderNotFound"));


            if (request.Status == ShipmentStatus.Delivered)
                order.OrderStatus = OrderStatus.delivered;
            await ordersService.UpdateAsync(order);
            await shipmentTrackingService.UpdateAsync(shipmenttracking);
            await shipmentTrackingService.SaveChangesAsync();

            return Success(localization.Get("ShipmentStatusUpdatedSuccessfully"));
        }
    
      

        
        #endregion
    }
}
