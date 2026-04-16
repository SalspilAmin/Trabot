using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Data.Entities;



namespace Tradify.Core.Mapping.ProductMapping
{
    public partial class ProductProfile
    {
        public void GetProductByIdMapping()
        {
            
            CreateMap<Products, GetProductByIdResponse>()


                .ForMember(dest => dest.CategoryName,opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))

                .ForMember(dest => dest.AverageRating,opt => opt.MapFrom(src =>src.Reviews.Any()? src.Reviews.Average(r => (double)r.Rating): 0))

                .ForMember(dest => dest.ReviewsCount,opt => opt.MapFrom(src => src.Reviews != null ? src.Reviews.Count : 0))

              
               .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.ProductImages.OrderByDescending(i => i.IsMain).ThenBy(i => i.Id)))



               .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews.OrderByDescending(i => i.Id)));


            CreateMap<ProductImage, ProductImageResponse>()
                .ForMember(dest => dest.MediaPath,
                    opt => opt.MapFrom(src => src.MediaPath));


            


            CreateMap<Reviews, ProductReviewResponse>()
      .ForMember(dest => dest.UserName,
          opt => opt.MapFrom(src => src.Customer != null ? src.Customer.UserName : "Unknown"));


        }

    }
}
