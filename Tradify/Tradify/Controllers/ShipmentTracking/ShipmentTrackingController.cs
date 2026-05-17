//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Tradify.Bases;
//using Tradify.Core.Features.Order.Queries.Models;
//using Tradify.Core.Features.Product.Commands.Models;
//using Tradify.Core.Features.ShipmentTrackings.Commands.Models;
//using Tradify.Core.Features.ShipmentTrackings.Queries.Models;
//using Tradify.Data.AppMetaData;

//namespace Tradify.Controllers.ShipmentTracking
//{
//    [ApiController]
//    public class ShipmentTrackingController : AppControllerBase
//    {
//        [HttpPost(Router.ShipmentTracking.Add)]
//        public async Task<IActionResult> Add([FromRoute] int id)
//        {
//            var response = await Mediator.Send(new CreateShipmentCommand(id));
//            return NewResult(response);
//        }

//        [HttpPut(Router.ShipmentTracking.UpdateStatus)]
//        public async Task<IActionResult> UpdateShipmentStatus([FromRoute] int Id, [FromForm] UpdateShipmentStatusCommand command)
//        {
//            command.ShipmentTrackingId = Id;

//            var response = await Mediator.Send(command);
//            return NewResult(response);

//        }

//        [HttpGet(Router.ShipmentTracking.GetShipmentByOrder)]

//        public async Task<IActionResult> GetByOrder([FromRoute] int id)
//        {

//            var result = await Mediator.Send(new GetShipmentByOrderIdQuery(id));
//            return NewResult(result);
//        }
//    }
//}
