using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Data.Entities;

namespace Tradify.Core.Features.Dashpoard.ProductSeller.Queries.Results
{
    public class TopProductSellingResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal FinalPrice { get; set; }

        public double Rating { get; set; }

        public int ReviewsCount { get; set; }

        public ProductImageResponse MainImage { get; set; }

        public int SalesCount { get; set; }

    }

}
