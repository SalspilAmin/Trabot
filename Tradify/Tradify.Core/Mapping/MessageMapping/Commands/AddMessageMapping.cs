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
            CreateMap<MessageResult, MessageNotificationResult>()
               .ForMember(
                   dest => dest.MessageId,
                   opt => opt.MapFrom(src => src.Id)
               );
            CreateMap<Message, MessageNotificationResult>()
           .ForMember(dest => dest.MessageId,
               opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.SenderId,
               opt => opt.MapFrom(src => src.SenderId))
           .ForMember(dest => dest.ReceiverId,
               opt => opt.MapFrom(src => src.ReceiverId))
           .ForMember(dest => dest.Content,
               opt => opt.MapFrom(src => src.Content))
           .ForMember(dest => dest.CreatedAt,
               opt => opt.MapFrom(src => src.CreatedAt));
        }
    }
}
