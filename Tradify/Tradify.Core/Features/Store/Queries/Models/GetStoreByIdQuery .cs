using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Core.Features.Store.Queries.Results;

namespace Tradify.Core.Features.Store.Queries.Models
{
    public class GetStoreByIdQuery : IRequest<Response<GetStoreByIdResponse>>
    {

        public int Id { get; set; }
        public GetStoreByIdQuery(int id)
        {
            Id = id;
        }

    }
}
