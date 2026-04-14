using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Seller.Command.Models;
using Tradify.Core.Features.User.Commands.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers
{
   
    public class ManageSellerController : AppControllerBase
    {
        [HttpPost(Router.Seller.Create)]

        public async Task<IActionResult> Create([FromForm] AddSellerCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

    }
}
