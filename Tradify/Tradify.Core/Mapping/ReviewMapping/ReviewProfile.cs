using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Mapping.ReviewMapping
{
    public partial class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            AddReviewMapping();
            ReviewsMapping();
        }
    }
    
}
