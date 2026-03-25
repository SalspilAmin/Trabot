using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Core.Features.ProductVariant.Queries.Results;
using Tradify.Core.Features.User.Queries.Results;
using Tradify.Core.Wrappers;

namespace Tradify.Core.Features.ProductVariant.Queries.Models
{
    public class GetProductVariantsByProductQuery : IRequest<Response<PaginatedResult<GetProductVariantByProductResponse>>>
    {
        public int ProductId { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }


        public bool? Discount { get; set; }

        public string? Search { get; set; }
        public bool? IsDeleted { get; set; } 
       
        public bool? OutOfStock { get; set; }
        

    }
}
