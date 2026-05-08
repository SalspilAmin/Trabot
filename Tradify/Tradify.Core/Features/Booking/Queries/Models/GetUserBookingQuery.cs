using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Booking.Queries.Results;
using Tradify.Core.Features.InstructorSchedules.Queries.Results;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Core.Wrappers;

namespace Tradify.Core.Features.Booking.Queries.Models
{
    public class GetUserBookingQuery : IRequest<PaginatedResult<GetUserBookingResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
