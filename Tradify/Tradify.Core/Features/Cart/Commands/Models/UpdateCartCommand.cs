using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Cart.Queries.Results;

namespace Tradify.Core.Features.Cart.Commands.Models
{
    public class UpdateCartCommand : IRequest<Response<GetCartByUserIdQueryResult>>
    {
        public int UserId { get; set; }
        public int CartId { get; set; }
        public List<CartProductResult>? ProductsInCart { get; set; }


    }
}
