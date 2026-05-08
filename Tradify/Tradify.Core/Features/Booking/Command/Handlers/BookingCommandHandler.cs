using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Booking.Command.Models;
using Tradify.Core.Features.InstructorSchedules.Command.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Enums;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;

namespace Tradify.Core.Features.Booking.Command.Handlers
{
    public class BookingCommandHandler : ResponseHandler,
                                         IRequestHandler<AddBookingCommand, Response<string>>,
                                         IRequestHandler<CancelBookingCommand, Response<string>>,
                                         IRequestHandler<RescheduleBookingCommand, Response<string>>






    {
        #region Fields
        private readonly LocalizationService localize;
        private readonly IMapper mapper;
        private readonly IBookingsService bookingsService;
        private readonly ICurrentUserService currentUserService;    

        #endregion

        #region Constructor
        public BookingCommandHandler(IMapper mapper,
                                     LocalizationService localize,
                                    IBookingsService bookingsService
            , ICurrentUserService currentUserService) : base(localize)
        {
            this.mapper = mapper;
            this.localize = localize;
            this.bookingsService = bookingsService;
            this.currentUserService = currentUserService;   
        }
        #endregion

        #region Methods

        // Add education

        public async Task<Response<string>> Handle(AddBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = mapper.Map<Data.Entities.Appointments.Bookings>(request);


            var result = await bookingsService.AddBookingAsync(booking);

            if (result.Item1 != "Success")
            {
                return BadRequest<string>(localize.Get(result.Item1));
            }
            else
            {
                return Success<string>(localize.Get("Success"), meta: result.Item2);
            }


        }

        //Canceld Booking
        public async Task<Response<string>> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
        {
            var currantuser = currentUserService.GetUserId();
            var booking = await bookingsService.GetByIdAsync(request.BookingId);
            if (booking == null)
                return NotFound<string>(localize.Get("BookingNotFound"));

            if (booking.Status == BookingStatus.Cancelled)
                return BadRequest<string>(localize.Get("BookingAlreadyCancelled"));

            if (!(booking.CustomerId == currantuser) && !(booking.Instructor.UserId == currantuser))
                return Unauthorized<string>(localize.Get("YouDontHaveAuthorized")); ;

            if (booking.BookingDate <= DateTime.UtcNow)
                return BadRequest<string>(localize.Get("AppointmentAlreadyStarted"));

            booking.Status = BookingStatus.Cancelled;

            await bookingsService.UpdateAsync(booking); 

            await bookingsService.SaveChangesAsync();

            return Success("BookingCancelledSuccessfully");

        }


        // Rescedul Booking


        public async Task<Response<string>> Handle(RescheduleBookingCommand request, CancellationToken cancellationToken)
        {

            var result = await bookingsService.RescheduleBookingAsync(request.BookingId ,request.NewScheduleId);

            if (result != "Success")
            {
                return BadRequest<string>(localize.Get(result));
            }
            else
            {
                return Success<string>(localize.Get("Success"));
            }


        }

        #endregion
    }
}
