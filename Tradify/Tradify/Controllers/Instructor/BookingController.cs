using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Booking.Command.Models;
using Tradify.Core.Features.Booking.Queries.Models;
using Tradify.Core.Features.Certification.Command.Models;
using Tradify.Core.Features.Education.Queries.Models;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers.Instructor
{
    
    public class BookingController : AppControllerBase
    {
        [HttpPost(Router.Booking.Add)]
        public async Task<IActionResult> Add([FromForm] AddBookingCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpGet(Router.Booking.GetUserBookings)]
        public async Task<IActionResult> GetUserBookings([FromQuery] GetUserBookingQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet(Router.Booking.GetInstructorBooking)]
        public async Task<IActionResult> GetInstructorBooking([FromQuery] GetInstructorBookingQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPut(Router.Booking.CanceldBooking)]
        public async Task<IActionResult> CanceldBooking([FromForm] CancelBookingCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}
