using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Tradify.Core.Features.Seller.Queries.Results;
using Tradify.Core.Features.Store.Queries.Results;
using Tradify.Data.Entities;

namespace Tradify.Core.Mapping.SellerMapping
{
    public partial class SellerProflie 
    {
        public void GetSellerByIdMapping()
        {
            CreateMap<Sellers, GetSellerByIdResponse>()

                    .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))


                   .ForMember(dest => dest.BusinessName, src => src.MapFrom(x => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(x.BusinessName.ToLower())))

                   .ForMember(dest => dest.BusinessType, src => src.MapFrom(x => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(x.BusinessType.ToLower())))


                   .ForMember(dest => dest.IsActive, src => src.MapFrom(x => x.IsActive))
                   .ForMember(dest => dest.UserId, src => src.MapFrom(x => x.UserId))




                   .ForMember(dest => dest.UserName, src => src.MapFrom(x => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(x.User.UserName.ToLower())))

                   .ForMember(dest => dest.StoreName, src => src.MapFrom(x => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(x.Store.Name.ToLower())))

                   .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));




        }
    }
}
