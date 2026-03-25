using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Review.Queries.Results;
using Tradify.Core.Features.Review.Commands.Models;
using Tradify.Core.Features.Review.Queries.Models;
using Tradify.Data.Entities;

namespace Tradify.Core.Mapping.ReviewMapping
{
    public partial class ReviewProfile 
    {
        public void ProductReviewMapping()
        {
            CreateMap<Reviews ,ProductReviewsResponse>()
                
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Customer.UserName)); 

        }
    }
    
}
