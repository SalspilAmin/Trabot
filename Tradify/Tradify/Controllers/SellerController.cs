using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Instructor.Command.Models;
using Tradify.Core.Features.Seller.Command.Models;
using Tradify.Core.Features.Seller.Queries.Models;
using Tradify.Core.Features.Store.Queries.Models;
using Tradify.Core.Features.User.Commands.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers
{
   
    public class SellerController : AppControllerBase
    {

        [HttpPost(Router.Seller.Create)]

        public async Task<IActionResult> Create([FromForm] AddSellerCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpGet(Router.Seller.GetByID)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetSellerByIdQuery(id));
            return NewResult(result);
        }

        [HttpGet(Router.Seller.Paginated)]
        public async Task<IActionResult> GetSellerPagination([FromQuery] GetAllSellerQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }


        [HttpGet(Router.Seller.NotHaveStore)]
        public async Task<IActionResult> GetSellerNotHaveStore([FromQuery] GetSellerNotHaveStoreQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
        [HttpGet(Router.Seller.Profile)]
        public async Task<IActionResult> Profile([FromForm] GetSellerProfileQuery query)
        {
            var result = await Mediator.Send(query);
            return NewResult(result);
        }



        [HttpPut(Router.Seller.Update)]
        public async Task<IActionResult> Update([FromForm] UpdateSellerCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }
        [HttpPut(Router.Seller.DisActive)]
        public async Task<IActionResult> DisActive([FromRoute] int id)
        {
            var result = await Mediator.Send(new DisActiveSellerCommand(id));
            return NewResult(result);
        }
        [HttpPut(Router.Seller.Active)]
        public async Task<IActionResult> Active([FromRoute] int id)
        {
            var result = await Mediator.Send(new ActiveSellerCommand(id));
            return NewResult(result);
        }

    }
}
