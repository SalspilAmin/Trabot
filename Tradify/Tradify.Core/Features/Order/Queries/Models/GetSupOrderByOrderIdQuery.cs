using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.InstructorSchedules.Queries.Results;
using Tradify.Core.Features.Order.Queries.Results;

namespace Tradify.Core.Features.Order.Queries.Models
{
    public class GetSupOrderByOrderIdQuery : IRequest<List<GetSupOrderByOrderIdResponse>>
    {
        public int OrderId { get; set; } //OrderId
        public GetSupOrderByOrderIdQuery(int orderId)
        {
            OrderId = orderId;
        }
    }
}
