using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Core.Features.User.Queries.Results;
using Tradify.Core.Wrappers;

namespace Tradify.Core.Features.Product.Queries.Models
{
    public class GetProductPaginationQuery : IRequest<Response<GetProductPaginationWrapper>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? StoreId { get; set; }   
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public int? CategoryId { get; set; }

        public int? MinRating { get; set; }
        public bool? Discount { get; set; }

        public string? Search { get; set; }
       
    }
}
     
