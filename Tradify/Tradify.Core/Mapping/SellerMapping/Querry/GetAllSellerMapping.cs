using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Tradify.Core.Features.Seller.Queries.Results;
using Tradify.Data.Entities;

namespace Tradify.Core.Mapping.SellerMapping
{
    public partial class SellerProflie : Profile
    {
        public void GetAllSellerMapping()
        {

            CreateMap<Sellers, GetAllSellerResponse>()

                   .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))


                  .ForMember(dest => dest.BusinessName, src => src.MapFrom(x => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(x.BusinessName.ToLower())))

                  .ForMember(dest => dest.BusinessType, src => src.MapFrom(x => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(x.BusinessType.ToLower())))


                  .ForMember(dest => dest.IsActive, src => src.MapFrom(x => x.IsActive));







        }
    }
}
