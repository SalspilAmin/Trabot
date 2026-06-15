using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Dashpoard.Admin.Queries.Models;
using Tradify.Core.Features.Dashpoard.ProductSeller.Queries.Models;
using Tradify.Data.AppMetaData;
using Tradify.Data.Helpers;

namespace Tradify.Controllers.Dashpoard
{
    [Authorize(Roles = RolesHelper.Seller)]
    public class SellerProductDashpoardController : AppControllerBase
    {

        [HttpGet(Router.Dashpoard.SellerProduct)]
        public async Task<IActionResult> GetSellerProductDashboard()
        {
            var result = await Mediator.Send(new GetSellerProductDashboardQuery());
            return NewResult(result);
        }


         [HttpGet(Router.Dashpoard.SellerProductOrdersChart)]
        public async Task<IActionResult> GetSellerProductOrdersChart()
        {
            var result = await Mediator.Send(new GetSellerOrdersChartQuery());
            return NewResult(result);
        }

        [HttpGet(Router.Dashpoard.SellerProductRevenueChart)]
        public async Task<IActionResult> GetSellerProductRevenueChart()
        {
            var result = await Mediator.Send(
                new GetSellerRevenueChartQuery());

            return NewResult(result);
        }


        [HttpGet(Router.Dashpoard.TopProductsSelling)]
        public async Task<IActionResult> GetTopProductsSelling([FromQuery] GetTopProductsSellingQuery query)
        {
            var result = await Mediator.Send(query);

            return NewResult(result);
        }

        [HttpGet(Router.Dashpoard.TopRatedProducts)]
        public async Task<IActionResult> GetTopRatedProducts([FromQuery] GetTopRatedProductsQuery query)
        {
            var result = await Mediator.Send(query);

            return NewResult(result);
        }
    }
}
