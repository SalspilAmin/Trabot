using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Core.Features.Store.Queries.Results;

namespace Tradify.Core.Features.Product.Queries.Models
{
    public class GetAllProductListQuery : IRequest<List<GetProductPaginationReponse>>
    {
       // public int UserId { get; set; } 
        
    }
}
