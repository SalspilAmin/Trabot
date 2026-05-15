using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Seller.Queries.Results;
using Tradify.Core.Features.Store.Queries.Results;

namespace Tradify.Core.Features.Seller.Queries.Models
{
    public class GetSellerByIdQuery : IRequest<Response<GetSellerByIdResponse>>
    {

        public int Id { get; set; }
        public GetSellerByIdQuery(int id)
        {
            Id = id;
        }

    }
}
