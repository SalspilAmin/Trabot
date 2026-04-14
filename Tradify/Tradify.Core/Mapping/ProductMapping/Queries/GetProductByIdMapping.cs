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


                .ForMember(dest => dest.CategoryName,
            opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.AverageRating,
    opt => opt.MapFrom(src =>
        src.Reviews.Any()
        ? src.Reviews.Average(r => (double)r.Rating)
        : 0))

                .ForMember(dest => dest.ReviewsCount,
    opt => opt.MapFrom(src => src.Reviews.Count))

              .ForMember(dest => dest.IsFavorite,
    opt => opt.MapFrom((src, dest, _, context) =>
        context.Items.ContainsKey("CurrentUserId") &&
        context.Items["CurrentUserId"] != null &&
        src.Favorites != null &&
        src.Favorites.Any(f => f.UserId == (int)context.Items["CurrentUserId"])))


       .ForMember(dest => dest.Images,
    opt => opt.MapFrom(src => src.ProductImages.OrderByDescending(i => i.IsMain).ThenBy(i => i.Id)))

        .ForMember(dest => dest.Videos,
            opt => opt.MapFrom(src => src.ProductVideos))

        .ForMember(dest => dest.Variants,
            opt => opt.MapFrom(src => src.ProductVariants))

        .ForMember(dest => dest.Reviews,
            opt => opt.MapFrom(src => src.Reviews));


            CreateMap<ProductImage, ProductImageResponse>()
                .ForMember(dest => dest.MediaPath,
                    opt => opt.MapFrom(src => src.MediaPath));


            CreateMap<ProductVideo, ProductVideoResponse>()
                .ForMember(dest => dest.MediaPath,
                    opt => opt.MapFrom(src => src.MediaPath));


            CreateMap<ProductVariants, ProductVariantResponse>()
                 .ForMember(dest => dest.FinalPrice,
                    opt => opt.MapFrom(src => src.FinalPrice))
                .ForMember(dest => dest.NumberOfProductInStock,
                    opt => opt.MapFrom(src => src.NumberOfProductInStock));


            CreateMap<Reviews, ProductReviewResponse>()
      .ForMember(dest => dest.UserName,
          opt => opt.MapFrom(src => src.Customer != null ? src.Customer.UserName : "Unknown"));


        }

    }
}
