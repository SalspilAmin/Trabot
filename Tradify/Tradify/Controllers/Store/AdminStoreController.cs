using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Features.Store.Commands.Models;
using Tradify.Core.Features.Store.Queries.Models;
using Tradify.Data.AppMetaData;
using Tradify.Data.Entities;

namespace Tradify.Controllers.Store
{
   
    [ApiController]
    public class AdminStoreController : AppControllerBase
    {
        [HttpPost(Router.Store.Add)]
        public async Task<IActionResult> Add([FromForm] AddStoreCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        
        [HttpDelete(Router.Store.Delete)]
        public async Task<IActionResult> DeleteStore([FromBody] DeleteStoreCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPut(Router.Store.restore)] 
        public async Task<IActionResult> RestoreStore([FromBody] RestoreStoreCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        
        [HttpGet(Router.Store.GetDeletedStores)]
        public async Task<IActionResult> GetDeletedStores([FromQuery] GetDeletedStoresQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
      
    }
}
