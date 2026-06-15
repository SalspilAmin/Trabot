using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Dashpoard.Admin.Queries.Models;
using Tradify.Core.Features.Product.Queries.Models;
using Tradify.Data.AppMetaData;
using Tradify.Data.Entities;
using Tradify.Data.Helpers;

namespace Tradify.Controllers.Dashpoard
{
  //  [Authorize(Roles = RolesHelper.Admin)]

    public class AdminDashpoardController : AppControllerBase
    {

    

        [HttpGet(Router.Dashpoard.Admin)]
        public async Task<IActionResult> GetAdminDashboard()
        {
            var result = await Mediator.Send(new GetAdminDashboardQuery());
            return NewResult(result);
        }


        [HttpGet(Router.Dashpoard.OrdersChart)]
        public async Task<IActionResult> GetAdminOrdersChart()
        {
            var result = await Mediator.Send(new GetAdminOrdersChartQuery());
            return NewResult(result);
        }

        [HttpGet(Router.Dashpoard.BookingChart)]
        public async Task<IActionResult> GetAdminBookingChart()
        {
            var result = await Mediator.Send(new GetAdminBookingsChartQuery());
            return NewResult(result);
        }
        [HttpGet(Router.Dashpoard.RevenueChart)]
        public async Task<IActionResult> GetAdminRevenueChart()
        {
            var result = await Mediator.Send(
                new GetAdminRevenueChartQuery());

            return NewResult(result);
        }


        [HttpGet(Router.Dashpoard.TopStores)]
        public async Task<IActionResult> GetTopStores([FromQuery] GetTopStoresQuery query)
        {
            var result = await Mediator.Send(query);

            return NewResult(result);
        }
    }
}
