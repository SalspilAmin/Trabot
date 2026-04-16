using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Features.Product.Queries.Results
{
    public  class GetProductByIdResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }
        public double AverageRating { get; set; }

        public int ReviewsCount { get; set; }

        public bool IsFavorite { get; set; }

        public ICollection<ProductImageResponse>? Images { get; set; }


        public ICollection<ProductReviewResponse>? Reviews { get; set; }
        
    }
    public class ProductImageResponse
    {
        public int Id { get; set; }

        public string MediaPath { get; set; }

        public bool IsMain { get; set; }
    }
    public class ProductReviewResponse
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; }
    }
  
  
}
