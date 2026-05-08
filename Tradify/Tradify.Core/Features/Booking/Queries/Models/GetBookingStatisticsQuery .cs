using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Booking.Queries.Results;

namespace Tradify.Core.Features.Booking.Queries.Models
{
    public class GetBookingStatisticsQuery : IRequest<Response<GetBookingStatisticsResponse>>
    {
    }
}
