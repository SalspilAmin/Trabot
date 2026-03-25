using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Core.Wrappers;

namespace Tradify.Core.Features.Product.Queries.Models
{
    public class GetSellerProductsQuery : IRequest<Response<PaginatedResult<GetSellerProductPaginationReponse>>>
    {
        public string? Search { get; set; }

        public int? CategoryId { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public bool? OutOfStock { get; set; }

        public bool? Discount { get; set; }

        public bool? IsDeleted { get; set; }

     
        public int PageNumber { get; set; } 
        public int PageSize { get; set; }
       

       
        
    }
}
