using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Interactions.Commands.Models;
using Tradify.Data.Entities.Posts;
using Tradify.Data.Helpers.Results;

namespace Tradify.Core.Mapping.Interactions
{
    public partial class InteractionProfile
    {
        public void AddInteraction()
        {
            CreateMap<AddInteractionWithPostCommand, InteractionWithPost>();
            CreateMap<InteractionWithPost, InteractionWithPostResult>()
            .ForMember(dest => dest.UserName,
              opt => opt.MapFrom(src => src.User != null
            ? src.User.UserName
            : string.Empty));
        }
    }
}
