using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Mapping.AuthorizationMapping
{
    public partial  class AuthorizationProfile : Profile
    {
        public AuthorizationProfile() 
        {
             GetAllRolesMapping();
            GetRoleMapping();
        }
    }
}
