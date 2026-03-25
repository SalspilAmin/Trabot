using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Features.Review.Queries.Results
{
    public class ProductReviewsResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;    
        public string Comment { get; set; } = string.Empty;   
        public int Rating { get; set; }
        public bool IsPurchased { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
 