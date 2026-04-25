using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Features.Product.Queries.Results
{
    public class GetSellerProductPaginationReponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal FinalPrice { get; set; }

        public double Rating { get; set; }

        public int ReviewsCount { get; set; }

        public ProductImageResponse MainImage { get; set; }


    }
}
