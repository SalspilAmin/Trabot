using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Core.Features.User.Queries.Results;
using Tradify.Data.Entities;
using Tradify.Data.Entities.Identity;


namespace Tradify.Core.Mapping.ProductMapping
{
    public partial class ProductProfile
    {
        public void GetProductPaginationMapping() 
        {
            CreateMap<Products, GetProductPaginationReponse>()

                 .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))

                 .ForMember(dest => dest.FinalPrice, src => src.MapFrom(x => x.FinalPrice))

                 .ForMember(dest => dest.InStock, src => src.MapFrom(x => x.InStock))

                 .ForMember(dest => dest.NumberOfProductInStock, src => src.MapFrom(x => x.NumberOfProductInStock))

                 .ForMember(dest => dest.Rating, src => src.MapFrom(x => x.Reviews != null && x.Reviews.Any() ? x.Reviews.Average(r => (double)r.Rating) : 0))
                 
                 .ForMember(dest => dest.ReviewsCount, src => src.MapFrom(x => x.Reviews != null ? x.Reviews.Count : 0))
                 
                 .ForMember(dest => dest.ImageUrl, src => src.MapFrom(x => x.ProductImages != null && x.ProductImages.Any() ? x.ProductImages.First().MediaPath : null));
        }
    }
}
