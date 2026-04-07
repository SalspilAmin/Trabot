using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Authorization.Queries.Results;
using Tradify.Data.Entities.Identity;

namespace Tradify.Core.Mapping.AuthorizationMapping
{
    public partial class AuthorizationProfile
    {
        public void GetAllRolesMapping()
        {
            CreateMap<Role, GetRolesListResult>();

        }
        public void GetRoleMapping() { CreateMap<Role, GetRoleByIdResult>(); }
    }
}
