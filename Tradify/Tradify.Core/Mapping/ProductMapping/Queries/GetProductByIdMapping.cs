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

                    .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))

                    .ForMember(dest => dest.Description, src => src.MapFrom(x => x.Description))


                    .ForMember(dest => dest.FinalPrice, src => src.MapFrom(x => x.FinalPrice))

                    .ForMember(dest => dest.InStock, src => src.MapFrom(x => x.InStock))

                    .ForMember(dest => dest.NumberOfProductInStock, src => src.MapFrom(x => x.NumberOfProductInStock))

                    .ForMember(dest => dest.Rating, src => src.MapFrom(x => x.Reviews != null && x.Reviews.Any() ? x.Reviews.Average(r => (double)r.Rating) : 0))

                    .ForMember(dest => dest.ReviewsCount, src => src.MapFrom(x => x.Reviews != null ? x.Reviews.Count : 0))

                    .ForMember(dest => dest.ImageUrls, src => src.MapFrom(x => x.ProductImages != null ? x.ProductImages.Select(i => i.MediaPath).ToList() : new List<string>()));
          
                    
                            
        }

    }
}
