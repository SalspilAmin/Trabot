using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Product.Queries.Results;

namespace Tradify.Core.Features.Product.Queries.Models
{
    public class GetProductByStoreQuery : IRequest<List<GetProductPaginationReponse>>
    {
          public int UserId { get; set; } 
        public int StoreId { get; set; }
    }
}
