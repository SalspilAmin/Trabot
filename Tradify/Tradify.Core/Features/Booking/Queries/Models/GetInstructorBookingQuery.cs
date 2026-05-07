using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Booking.Queries.Results;
using Tradify.Core.Wrappers;

namespace Tradify.Core.Features.Booking.Queries.Models
{
    public class GetInstructorBookingQuery : IRequest<PaginatedResult<GetInstructorBookingResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
