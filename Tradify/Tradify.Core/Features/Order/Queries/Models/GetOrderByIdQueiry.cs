using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Order.Queries.Results;

namespace Tradify.Core.Features.Order.Queries.Models
{
    public class GetOrderByIdQueiry : IRequest<Response<OrderResultQueiry>>
    {
        public int Id { get; set; }
        public GetOrderByIdQueiry(int id)
        {
            Id = id;
        }
    }
}
