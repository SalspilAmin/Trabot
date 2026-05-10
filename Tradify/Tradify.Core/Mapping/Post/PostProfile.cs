using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Mapping.Post
{
    public partial class PostProfile : Profile
    {
        public PostProfile()
        {
            AddPostMapping();
            GetListOfPostsByUserMap();
        }
    }
}
