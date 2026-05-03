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
        public void ReviewsMapping()
        {
            CreateMap<Reviews ,ReviewsResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment)) 
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating)) 
            .ForMember(dest => dest.IsMyReview, opt => opt.MapFrom(src => src.IsPurchased)) 
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Customer.UserName)); 

        }
    }
    
}
