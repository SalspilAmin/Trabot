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
        public int? StoreId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public int? CategoryId { get; set; }

        public int? MinRating { get; set; }
        public bool? Discount { get; set; }

        public string? Search { get; set; }
    }
}
