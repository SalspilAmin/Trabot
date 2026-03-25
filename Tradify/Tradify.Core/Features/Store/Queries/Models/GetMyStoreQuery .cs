using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Store.Queries.Results;

namespace Tradify.Core.Features.Store.Queries.Models
{
    public class GetMyStoreQuery : IRequest<Response<GetStoreByIdResponse>>
    {
        public int SellerId { get; set; }
        public GetMyStoreQuery(int id)
        {
            SellerId = id;
        }
    }
    
}
