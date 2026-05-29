using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Comments.Commands.Models;
using Tradify.Data.Entities.Comments;
using Tradify.Data.Helpers.Results;

namespace Tradify.Core.Mapping.Comments
{
    public partial class CommentProfile
    {
        public void AddCommentMapping()
        {

            CreateMap<AddCommentCommand, Comment>();

            CreateMap<Comment, CommentResult>()
                .ForMember(dest => dest.UserName,
                    opt => opt.MapFrom(src => src.User.UserName)).ForMember(dest => dest.ReplayOnCommentNumber,
                    opt => opt.MapFrom(src =>
                        src.ReplyOFComments != null ? src.ReplyOFComments.Count : 0));
            CreateMap<ReplyOFComment, ReplyCommentResult>()
    .ForMember(dest => dest.UserName,
        opt => opt.MapFrom(src =>
            src.UserThatWriteAReplyOFComment.UserName));

            CreateMap<AddReplyCommentCommand,
                ReplyOFComment>();

            CreateMap<ReplyOFComment,
                ReplyCommentResult>()
                .ForMember(dest => dest.UserName,
                    opt => opt.MapFrom(src =>
                        src.UserThatWriteAReplyOFComment.UserName));

        }
    }
}
