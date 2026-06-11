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
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Core.Features.ShipmentTrackings.Commands.Handlers
{
    public class ShipmentTrackingCommandHandler : ResponseHandler, IRequestHandler<CreateShipmentCommand, Response<string>>
                                                                 , IRequestHandler<UpdateShipmentStatusCommand, Response<string>>





    {
        #region fields
        private readonly IMapper mapper;
        private readonly LocalizationService localize;
        private readonly ICurrentUserService currentUserService;
        private readonly IShipmentService shipmentService;
        private readonly IOrdersService ordersService;
        private readonly IShipmentTrackingService shipmentTrackingService;




        #endregion

        #region Constructor

        public ShipmentTrackingCommandHandler(LocalizationService localize,
             IMapper mapper, ICurrentUserService currentUserService
            , IOrdersService ordersService, IShipmentService shipmentService,
             IShipmentTrackingService shipmentTrackingService) : base(localize)
        {
            this.mapper = mapper;
            this.localize = localize;
            this.currentUserService = currentUserService;
            this.ordersService = ordersService;
            this.shipmentTrackingService = shipmentTrackingService;
            this.shipmentService = shipmentService;

        }

        #endregion

        #region Mehtods


        public async Task<Response<string>> Handle(CreateShipmentCommand request, CancellationToken cancellationToken)
        {

            var result = await shipmentService.CreateShipment(request.SubOrderId);

            if (result.Item1 != "Success")
            {
                return BadRequest<string>(localize.Get(result.Item1));
            }
            else
            {
                return Success<string>("Success",
                                                 meta: new
    {
        ShipmentId = result.Item2,
        ShipmentTrackingId = result.Item3
    });
            }
        }



        public async Task<Response<string>> Handle(UpdateShipmentStatusCommand request, CancellationToken cancellationToken)
        {
            var result = await shipmentService.UpdateShipmentStatus(request.ShipmentId,request.Status);

            if (result != "Success")
            {
                return BadRequest<string>(localize.Get(result));
            }
            else
            {
                return Success<string>("Success");
            }
        }




        #endregion
    }
}
