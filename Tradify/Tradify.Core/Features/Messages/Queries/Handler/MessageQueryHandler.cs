using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Messages.Queries.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Helpers.Results;
using Tradify.Service.AbstractsServices;

namespace Tradify.Core.Features.Messages.Queries.Handler
{
    public class MessageQueryHandler :
      ResponseHandler,

      IRequestHandler<
          GetMessageByIdQuery,
          Response<MessageResult>>,

      IRequestHandler<
          GetConversationQuery,
          Response<List<MessageResult>>>,

      IRequestHandler<
          GetUnreadMessagesQuery,
          Response<List<MessageResult>>>
    {
        private readonly IMessageService messageService;
        private readonly IMapper mapper;

        public MessageQueryHandler(
            IMessageService messageService,
            IMapper mapper,
            LocalizationService localization)
            : base(localization)
        {
            this.messageService = messageService;
            this.mapper = mapper;
        }

        #region Get Message By Id

        public async Task<Response<MessageResult>> Handle(
            GetMessageByIdQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var message =
                    await messageService.GetByIdAsync(
                        request.MessageId);

                if (message == null||message.IsDeleted==true)
                    return NotFound<MessageResult>(
                        "Message Not Found");

                var result =
                    mapper.Map<MessageResult>(
                        message);

                return Success(result);
            }
            catch (Exception ex)
            {
                return BadRequest<MessageResult>(
                    ex.Message);
            }
        }

        #endregion

        #region Get Conversation

        public async Task<Response<List<MessageResult>>> Handle(
            GetConversationQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var messages =
                    await messageService.GetConversationAsync(
                        request.SenderId,
                        request.ReceiverId);

                var result =
                    mapper.Map<List<MessageResult>>(
                        messages);

                return Success(result);
            }
            catch (Exception ex)
            {
                return BadRequest<List<MessageResult>>(
                    ex.Message);
            }
        }

        #endregion

        #region Get UnRead Messages

        public async Task<Response<List<MessageResult>>> Handle(
            GetUnreadMessagesQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var messages =
                    await messageService.GetUnreadMessagesAsync(
                        request.UserId);

                var result =
                    mapper.Map<List<MessageResult>>(
                        messages);

                return Success(result);
            }
            catch (Exception ex)
            {
                return BadRequest<List<MessageResult>>(
                    ex.Message);
            }
        }

        #endregion
    }
}
