using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Post.Queries.Results;
using Tradify.Data.Entities.Posts;
using Tradify.Data.Helpers.Results;

namespace Tradify.Core.Mapping.Post
{
    public partial class PostProfile
    {
        public void GetListOfPostsByUserMap()
        {

            CreateMap<ImageOrVideoPath, ImageOrVideoPathResults>();

            CreateMap<Tradify.Data.Entities.Posts.Post, GetListOfPostsResult>()
                .ForMember(dest => dest.CommentsNumber,
                    opt => opt.MapFrom(src =>
                        src.Comments != null ? src.Comments.Count : 0))
                      .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src => src.User != null ? src.User.UserName : null))

                .ForMember(dest => dest.interactionWithPostsNumber,
                    opt => opt.MapFrom(src =>
                        src.interactionWithPosts != null ? src.interactionWithPosts.Count : 0))

                .ForMember(dest => dest.ImageOrVideo_Paths,
                    opt => opt.MapFrom(src => src.ImageOrVideo_Paths));

            CreateMap<Tradify.Data.Entities.Posts.Post, PostResult>()
                .ForMember(dest => dest.CommentsNumber,
                    opt => opt.MapFrom(src =>
                        src.Comments != null ? src.Comments.Count : 0))

                .ForMember(dest => dest.interactionWithPostsNumber,
                    opt => opt.MapFrom(src =>
                        src.interactionWithPosts != null ? src.interactionWithPosts.Count : 0))
                  .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src => src.User != null ? src.User.UserName : null))
                .ForMember(dest => dest.ImageOrVideo_Paths,
                    opt => opt.MapFrom(src => src.ImageOrVideo_Paths)).ReverseMap();
                        










        }
    }
}
