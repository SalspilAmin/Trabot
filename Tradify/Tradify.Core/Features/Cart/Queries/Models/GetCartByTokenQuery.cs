using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Cart.Queries.Results;

namespace Tradify.Core.Features.Cart.Queries.Models
{
    public class GetCartByTokenQuery : IRequest<Response<GetCartByUserIdQueryResult>>
    {
        public string Token { get; set; }
        public GetCartByTokenQuery(string Token)
        { 
            this.Token= Token;
        }   
    }
}
