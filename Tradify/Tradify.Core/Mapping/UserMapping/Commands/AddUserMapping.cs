using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.User.Commands.Models;
using Tradify.Data.Entities.Identity;

namespace Tradify.Core.Mapping.UserMapping
{
    public partial class UserProfile
    {
        public void AddUserMapping()
        {
            CreateMap<AddUserCommand,User>().ForMember(dest=> dest.UserName,opt=>opt.MapFrom(x=>x.UserName))
                .ForMember(dest=>dest.Email,opt=>opt.MapFrom(x=>x.EmailOrPhone))
                .ForMember(dest=>dest.PhoneNumber,opt=>opt.MapFrom(x=>x.EmailOrPhone));
        }
    }
}
