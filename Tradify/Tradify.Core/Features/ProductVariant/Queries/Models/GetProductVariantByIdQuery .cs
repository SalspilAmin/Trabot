using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Core.Features.ProductVariant.Queries.Results;

namespace Tradify.Core.Features.ProductVariant.Queries.Models
{
    public class GetProductVariantByIdQuery : IRequest<Response<GetProductVariantByIdResponse>>
    {

        public int Id { get; set; }
        public GetProductVariantByIdQuery(int id)
        {
            Id = id;
        }
    
    }
}
