using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Instructor.Queries.Results;
using Tradify.Core.Wrappers;

namespace Tradify.Core.Features.Product.Queries.Results
{
    public class GetProductPaginationReponse
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public decimal FinalPrice { get; set; }

        public double Rating { get; set; }

        public int ReviewsCount { get; set; }

    
        public bool IsFavorite { get; set; }

        public ProductImageResponse MainImage { get; set; }


    }
    public class GetProductPaginationWrapper
    {
        public decimal StoreMinPrice { get; set; }

        public decimal StoreMaxPrice { get; set; }

        public PaginatedResult<GetProductPaginationReponse> Products { get; set; }
    }
}
