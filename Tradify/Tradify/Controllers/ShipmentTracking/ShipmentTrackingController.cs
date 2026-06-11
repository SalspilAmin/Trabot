using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Education.Queries.Models;
using Tradify.Core.Features.Order.Queries.Models;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Features.ShipmentTrackings.Commands.Models;
using Tradify.Core.Features.ShipmentTrackings.Queries.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers.ShipmentTracking
{
    [ApiController]
    public class ShipmentTrackingController : AppControllerBase
    {
        [HttpPost(Router.ShipmentTracking.Add)]
        public async Task<IActionResult> Add([FromRoute] int subOrderId)
        {
            var response = await Mediator.Send(new CreateShipmentCommand(subOrderId));
            return NewResult(response);
        }

        [HttpPut(Router.ShipmentTracking.UpdateStatus)]
        public async Task<IActionResult> UpdateShipmentStatus( [FromForm] UpdateShipmentStatusCommand command)
        {

            var response = await Mediator.Send(command);
            return NewResult(response);

        }

        [HttpGet(Router.ShipmentTracking.GetShipmentBySubOrder)]

        public async Task<IActionResult> BySubOrder([FromRoute] int subOrderId)
        {

            var result = await Mediator.Send(new GetShipmentByOrderIdQuery(subOrderId));
            return NewResult(result);
        }

        [HttpGet(Router.ShipmentTracking.BySeller)]
        public async Task<IActionResult> GetSupOrderBySeller([FromQuery] GetSellerShipmentQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet(Router.ShipmentTracking.BySubShipment)]

        public async Task<IActionResult> BySubShipment([FromRoute] int shipmentId)
        {

            var result = await Mediator.Send(new GetShipmentTrackingByShipmentQuery(shipmentId));
            return Ok(result);
        }
      
    }
}
