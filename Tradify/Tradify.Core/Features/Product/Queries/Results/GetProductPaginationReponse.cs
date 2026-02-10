using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Features.Product.Queries.Results
{
    public class GetProductPaginationReponse
    {
      
        public string Name { get; set; }
  
        public double FinalPrice { get; set; }

        //public string? MainImageUrl { get; set; }

        public string? ImageUrl { get; set; }

        public double Rating { get; set; }

        public int ReviewsCount { get; set; }

        public bool InStock { get; set; }

        public int NumberOfProductInStock { get; set; }


    }
}
