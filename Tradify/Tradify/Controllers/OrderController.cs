using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Order.Commands.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : AppControllerBase
    {
        [HttpPost(Router.Order.CreateOrder)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderModel request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);
        }
    }
}
