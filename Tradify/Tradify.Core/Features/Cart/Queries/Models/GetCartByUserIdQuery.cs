using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Cart.Queries.Results;

namespace Tradify.Core.Features.Cart.Queries.Models
{
    public class GetCartByUserIdQuery : IRequest<Response<GetCartByUserIdQueryResult>>
    {
        public int UserId { get; set; }
        public GetCartByUserIdQuery(int userId)
        {
            UserId = userId;
        }   
    }
}
