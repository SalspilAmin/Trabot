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
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))

                 .ForMember(dest => dest.FinalPrice, src => src.MapFrom(x => x.ProductVariants != null && x.ProductVariants.Any() ? x.ProductVariants.Min(v => v.FinalPrice) : 0))

                 .ForMember(dest => dest.Rating, src => src.MapFrom(x => x.Reviews != null && x.Reviews.Any() ? x.Reviews.Average(r => (double)r.Rating) : 0))

                 .ForMember(dest => dest.ReviewsCount, src => src.MapFrom(x => x.Reviews != null ? x.Reviews.Count : 0))
                .ForMember(dest => dest.IsFavorite, opt => opt.MapFrom(src => false))
              

               .ForMember(dest => dest.MainImage, opt => opt.MapFrom(src => src.ProductImages.FirstOrDefault(i => i.IsMain)));





                
           



        }
    }
}
