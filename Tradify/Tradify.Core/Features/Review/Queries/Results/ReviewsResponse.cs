using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Enums;

namespace Tradify.Core.Features.Review.Queries.Results
{
    public class ReviewsResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }    
        public string? Comment { get; set; }  
        public RatingValue Rating { get; set; }
        public bool IsPurchased { get; set; }
        public bool IsMyReview { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
 