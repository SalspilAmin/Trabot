using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.User.Queries.Results;
using Tradify.Data.Entities.Identity;

namespace Tradify.Core.Mapping.UserMapping
{
    public partial class UserProfile
    {
        public void GetUserByIdMapping()
        {
            CreateMap<User, GetUserByIdResponse>().ForMember(dest => dest.UserName, src => src.MapFrom(x => x.UserName))
                .ForMember(dest => dest.Email, src => src.MapFrom(x => x.Email)).ForMember(dest => dest.Phone, src => src.MapFrom(x => x.PhoneNumber));

        }
    }
}
