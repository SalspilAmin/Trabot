using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Dashpoard.Queries.Models;
using Tradify.Core.Features.Product.Queries.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers
{
    public class DashpoardController : AppControllerBase
    {

    

        [HttpGet(Router.Dashpoard.Admin)]
        public async Task<IActionResult> GetAdminDashboard()
        {
            var result = await Mediator.Send(new GetAdminDashboardQuery());
            return NewResult(result);
        }
    }
}
