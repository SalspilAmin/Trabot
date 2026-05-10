using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Post.Commands.Models;
using Tradify.Data.Entities.Posts;

namespace Tradify.Core.Mapping.Post
{
    public partial class PostProfile
    {
        public void AddPostMapping()
        {
            CreateMap<AddPostModelCommand,Tradify.Data.Entities.Posts.Post>()
                 .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.IsUpdated, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.ImageOrVideo_Paths, opt => opt.Ignore())
            .ForMember(dest => dest.Comments, opt => opt.Ignore())
            .ForMember(dest => dest.interactionWithPosts, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore());


           
        }
    }
}
