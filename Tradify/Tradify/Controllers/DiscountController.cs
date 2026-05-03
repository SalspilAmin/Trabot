using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Discount.Instructor.Command.Models;
using Tradify.Core.Features.Discount.Product.Command.Models;
using Tradify.Core.Features.Discount.Varint.Command.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers
{
    
    public class DiscountController : AppControllerBase
    {
        [HttpPut(Router.ProductVariant.AddDiscount)]
        public async Task<IActionResult> AddVarintDiscount([FromForm] AddDiscountCommand command)
        {

            var response = await Mediator.Send(command);
            return NewResult(response);

        }
        [HttpDelete(Router.ProductVariant.DeleteDiscount)]
        public async Task<IActionResult> DeleteVarintDiscount([FromQuery] DeleteDiscountCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }


        [HttpPut(Router.Product.AddDiscount)]
        public async Task<IActionResult> AddProductDiscount([FromForm] AddProductDiscountCommand command)
        {

            var response = await Mediator.Send(command);
            return NewResult(response);

        }
        [HttpDelete(Router.Product.DeleteDiscount)]
        public async Task<IActionResult> DeleteProductDiscount([FromQuery] DeleteProductDiscountCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }


        [HttpPut(Router.Instructor.AddDiscount)]
        public async Task<IActionResult> AddInstructorDiscount([FromForm] AddInstructorDiscountCommand command)
        {

            var response = await Mediator.Send(command);
            return NewResult(response);

        }
        [HttpDelete(Router.Instructor.DeleteDiscount)]
        public async Task<IActionResult> DeleteInstructorDiscount([FromQuery] DeleteInstructorDiscountCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }


        [HttpPut(Router.Store.AddStoreServiceDiscount)]
        public async Task<IActionResult> AddStoreServiceDiscount([FromForm] AddStoreServiceDiscountCommand command)
        {

            var response = await Mediator.Send(command);
            return NewResult(response);

        }
        [HttpDelete(Router.Store.DeleteStoreServiceDiscount)]
        public async Task<IActionResult> DeleteStoreServiceDiscount([FromQuery] DeleteStoreServiceDiscountCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }


    }
}
