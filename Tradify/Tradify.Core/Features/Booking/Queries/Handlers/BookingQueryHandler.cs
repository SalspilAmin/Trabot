using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Booking.Queries.Models;
using Tradify.Core.Features.Booking.Queries.Results;
using Tradify.Core.Features.Education.Queries.Models;
using Tradify.Core.Features.Education.Queries.Results;
using Tradify.Core.Features.Instructor.Queries.Models;
using Tradify.Core.Features.Instructor.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Core.Wrappers;
using Tradify.Data.Entities.Appointments;
using Tradify.Service.AbstractsServices;

namespace Tradify.Core.Features.Booking.Queries.Handlers
{
    public class BookingQueryHandler : ResponseHandler
                                                     , IRequestHandler<GetUserBookingQuery, PaginatedResult<GetUserBookingResponse>>
                                                     , IRequestHandler<GetInstructorBookingQuery, PaginatedResult<GetInstructorBookingResponse>>



    {
        #region fields
        private readonly IMapper mapper;
        private readonly LocalizationService localization;
        private readonly IInstructorsService instructorsService;
        private readonly ICurrentUserService currentUserService;
        private readonly IBookingsService bookingsService;
        #endregion

        #region Constructor
        public BookingQueryHandler(LocalizationService localization, IMapper mapper, IInstructorsService instructorsService
            , IBookingsService bookingsService ,ICurrentUserService currentUserService) : base(localization)
        {
            this.mapper = mapper;
            this.localization = localization;
            this.instructorsService = instructorsService;
            this.currentUserService = currentUserService;
            this.bookingsService = bookingsService;
        }

        #endregion

        #region Mehtods


        // Get User Booking

        public async Task<PaginatedResult<GetUserBookingResponse>> Handle(GetUserBookingQuery request, CancellationToken cancellationToken)
        {
            var currentUser = currentUserService.GetUserId();


            var userBookings =  bookingsService.GetTableNoTracking()
                .Where(e => e.CustomerId == currentUser);

            var result = await mapper
                                 .ProjectTo<GetUserBookingResponse>(userBookings)
                                 .ToPaginationlist(request.PageNumber, request.PageSize);
            return result;
        }



        // Get Instructor Booking

        public async Task<PaginatedResult<GetInstructorBookingResponse>> Handle(GetInstructorBookingQuery request, CancellationToken cancellationToken)
        {
            var currentUser = currentUserService.GetUserId();


            var userBookings = bookingsService.GetTableNoTracking()
                .Where(e => e.Instructor.UserId == currentUser);

            var result = await mapper
                                 .ProjectTo<GetInstructorBookingResponse>(userBookings)
                                 .ToPaginationlist(request.PageNumber, request.PageSize);
            return result;
        }



        #endregion
    }


}
