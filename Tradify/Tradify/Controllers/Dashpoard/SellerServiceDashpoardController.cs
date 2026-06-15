using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Dashpoard.ProductSeller.Queries.Models;
using Tradify.Core.Features.Dashpoard.ServiceSeller.Queries.Models;
using Tradify.Data.AppMetaData;
using Tradify.Data.Helpers;


namespace Tradify.Controllers.Dashpoard
{
    [Authorize(Roles = RolesHelper.Seller)]

    public class SellerServiceDashpoardController : AppControllerBase
    {
        [HttpGet(Router.Dashpoard.SellerService)]
        public async Task<IActionResult> GetSellerServiceDashboard()
        {
            var result = await Mediator.Send(new GetServiceSellerDashboardQuery());
            return NewResult(result);
        }


        [HttpGet(Router.Dashpoard.SellerServiceBookingChart)]
        public async Task<IActionResult> GetSellerServiceBookingChart()
        {
            var result = await Mediator.Send(new GetBookingsChartQuery());
            return NewResult(result);
        }

       


        [HttpGet(Router.Dashpoard.TopInstructor)]
        public async Task<IActionResult> GetTopInstructor([FromQuery] GetTopInstructorsQuery query)
        {
            var result = await Mediator.Send(query);

            return NewResult(result);
        }
    }
}
