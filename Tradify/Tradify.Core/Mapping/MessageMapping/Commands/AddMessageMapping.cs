using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Messages.Commands.Models;
using Tradify.Data.Entities.Chat;
using Tradify.Data.Helpers.Results;

namespace Tradify.Core.Mapping.MessageMapping
{
    public partial class MessageProfile
    {
        public void MessageMapping()
        {

            CreateMap<AddMessageCommand, Message>();

            CreateMap<Message, MessageResult>()

            .ForMember(
                dest => dest.SenderName,
                opt => opt.MapFrom(src =>
                    src.SenderUser.UserName))

            .ForMember(
                dest => dest.ReceiverName,
                opt => opt.MapFrom(src =>
                    src.ReceiverUser.UserName))

            .ForMember(
                dest => dest.MediaPaths,
                opt => opt.MapFrom(src =>
                    src.MessageMediaPaths
                        .Where(x => !x.IsDeleted)
                        .Select(x => x.MediaPath)));
        }
    }
}
