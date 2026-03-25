using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Product.Queries.Models;
using MediatR;
using Tradify.Data.AppMetaData;
using Tradify.Core.Features.Store.Queries.Models;

namespace Tradify.Controllers.Store
{
    
    [ApiController]
    public class StoreController : AppControllerBase
    {
        [HttpGet(Router.Store.GetByID)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetStoreByIdQuery(id));
            return NewResult(result);
        }

        [HttpGet(Router.Store.Paginated)]
        public async Task<IActionResult> GetStoresPagination([FromQuery] GetStoresPaginationQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }


}
