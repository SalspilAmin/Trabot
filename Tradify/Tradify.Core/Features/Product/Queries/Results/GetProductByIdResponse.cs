using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Features.Product.Queries.Results
{
    public  class GetProductByIdResponse
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public double FinalPrice { get; set; }

        public ICollection<string> ImageUrls { get; set; }


        public double Rating { get; set; }


        public int ReviewsCount { get; set; }

        public bool InStock { get; set; }

        public int NumberOfProductInStock { get; set; }
    }
}
