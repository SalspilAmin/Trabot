using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Mapping.UserMapping
{
    public partial class UserProfile :Profile
    {
        public UserProfile()
        {
            AddUserMapping();
        }
    }
}
