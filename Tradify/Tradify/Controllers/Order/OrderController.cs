using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Certification.Queries.Models;
using Tradify.Core.Features.Fawaterak.Comands.Models;
using Tradify.Core.Features.Order.Commands.Models;
using Tradify.Core.Features.Order.Queries.Models;
using Tradify.Core.Features.User.Commands.Models;
using Tradify.Core.Features.User.Queries.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers.Order
{
    
    [ApiController]
    public class OrderController : AppControllerBase
    {
        [HttpPost(Router.Order.CreateOrder)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderModel request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);
        }
        [HttpPut(Router.Order.UpdateOrder)]

        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderCommandModel request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);
        }
        [HttpGet(Router.Order.GetOrderById)]

        public async Task<IActionResult> GetById([FromRoute] int id) { 
        
            var result = await Mediator.Send(new GetOrderByIdQueiry(id));   
            return NewResult(result);       
        }

        [HttpGet(Router.Order.Paginated)]
        public async Task<IActionResult> GetCustomerOrderPagination([FromQuery] GetCustomerOrdersQuery request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete(Router.Order.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var resutl = await Mediator.Send(new DeleteOrderCommand(id));
            return Ok(resutl);
        }



      





    }

  
   


}
