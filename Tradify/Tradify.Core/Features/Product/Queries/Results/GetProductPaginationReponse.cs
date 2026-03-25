using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Features.Product.Queries.Results
{
    public class GetProductPaginationReponse
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string MainImage { get; set; }

        public decimal FinalPrice { get; set; }

        public double Rating { get; set; }

        public int ReviewsCount { get; set; }
 
        public bool IsFavorite { get; set; } 

    }
}
