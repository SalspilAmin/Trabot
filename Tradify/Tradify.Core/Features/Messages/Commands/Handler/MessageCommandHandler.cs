using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Messages.Commands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities.Chat;
using Tradify.Data.Helpers.Results;
using Tradify.RealTimeService.HubServices;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;

namespace Tradify.Core.Features.Messages.Commands.Handler
{
    public class MessageCommandHandler :
    ResponseHandler,

    IRequestHandler<
        AddMessageCommand,
        Response<int?>>,

    IRequestHandler<
        UpdateMessageCommand,
        Response<string>>,

    IRequestHandler<
        DeleteMessageCommand,
        Response<string>>,

    IRequestHandler<
        MarkMessageAsReadCommand,
        Response<string>>
    {
        private readonly IMessageService messageService;
        private readonly IMessageMediaPathService messageMediaPathService;
        private readonly IFileService fileService;
        private readonly IMapper mapper;
        private readonly MessageHubService chatHubService;

        public MessageCommandHandler(
            LocalizationService localization,
            IMessageService messageService,
            IMessageMediaPathService messageMediaPathService,
            IFileService fileService,
            IMapper mapper,
            MessageHubService chatHubService)
            : base(localization)
        {
            this.messageService = messageService;
            this.messageMediaPathService = messageMediaPathService;
            this.fileService = fileService;
            this.mapper = mapper;
            this.chatHubService = chatHubService;
        }

        #region Add Message

        public async Task<Response<int?>> Handle(
            AddMessageCommand request,
            CancellationToken cancellationToken)
        {
            using var transaction =
                await messageService.BeginTransactionAsync();

            try
            {
                var message =
                    mapper.Map<Message>(request);

                await messageService.AddAsync(message);

                await messageService.SaveChangesAsync();

                if (request.MediaFiles != null &&
                    request.MediaFiles.Any())
                {
                    foreach (var file in request.MediaFiles)
                    {
                        var uploadResult =
                            await fileService.UploadImageAsync(
                                file,
                                UploadFolder.Message.ToString());

                        if (uploadResult.Error != "Success")
                            continue;

                        await messageMediaPathService.AddAsync(
                            new MessageMediaPath
                            {
                                MessageId = message.Id,
                                MediaPath = uploadResult.Url
                            });
                    }

                    await messageMediaPathService.SaveChangesAsync();
                }

                var notifyResult =
              mapper.Map<MessageNotificationResult>(
                message);

                await chatHubService.NotifyNewMessage(
                    request.ReceiverId,
                    notifyResult);

                await chatHubService.NotifyNewMessage(
                    request.ReceiverId,
                    notifyResult);

                await transaction.CommitAsync();

                return Created<int?>(message.Id);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                return BadRequest<int?>(ex.Message);
            }
        }

        #endregion

        #region Update Message

        public async Task<Response<string>> Handle(
            UpdateMessageCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var message =
                    await messageService.GetByIdAsync(
                        request.MessageId);

                if (message == null)
                    return NotFound<string>("Message Not Found");

                message.Content = request.Content;
                message.IsUpdated = true;

                await messageService.SaveChangesAsync();

                return Success("Updated Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest<string>(ex.Message);
            }
        }

        #endregion

        #region Delete Message

        public async Task<Response<string>> Handle(
            DeleteMessageCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var message =
                    await messageService.GetByIdAsync(
                        request.MessageId);

                if (message == null)
                    return NotFound<string>("Message Not Found");

                message.IsDeleted = true;

                await messageService.SaveChangesAsync();

                return Success("Deleted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest<string>(ex.Message);
            }
        }

        #endregion

        #region Mark As Read

        public async Task<Response<string>> Handle(
            MarkMessageAsReadCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var message =
                    await messageService.GetByIdAsync(
                        request.MessageId);

                if (message == null)
                    return NotFound<string>("Message Not Found");

                message.IsRead = true;

                await messageService.SaveChangesAsync();
                await chatHubService.NotifyMessageRead(
                 message.SenderId,
                  message.Id);

                return Success("Message Read");
            }
            catch (Exception ex)
            {
                return BadRequest<string>(ex.Message);
            }
        }

        #endregion
    }
}
