using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Order.Queries.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers.Order
{
    [ApiController]
    public class SupOrderController : AppControllerBase
    {
        [HttpGet(Router.SupOrder.GetByOrder)]
        public async Task<IActionResult> GetSupOrderByOrder([FromQuery] int orderId)
        {
            var result = await Mediator.Send(new GetSupOrderByOrderIdQuery(orderId));
            return Ok(result);
        }

        [HttpGet(Router.SupOrder.BySeller)]
        public async Task<IActionResult> GetSupOrderBySeller([FromQuery] GetSellerSupOrderQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
