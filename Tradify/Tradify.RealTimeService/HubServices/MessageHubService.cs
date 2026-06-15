using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Chat;
using Tradify.Data.Entities.Posts;
using Tradify.Data.Helpers.Results;
using Tradify.Infrastructure.AbstractsRepositories.UserConnectionRepositories;

namespace Tradify.RealTimeService.HubServices
{
    public class MessageHubService
    {
        private readonly IUserConnectionRepository userConnectionRepository;
        private readonly IHubContext<Tradify.RealTimeService.HubNegotiation.HubNegotiation> hubContext;

        public MessageHubService(IUserConnectionRepository userConnection, IHubContext<Tradify.RealTimeService.HubNegotiation.HubNegotiation> hubcontext)
        {
            this.userConnectionRepository = userConnection;
            this.hubContext = hubcontext;
        }





        public async Task NotifyNewMessage(
        int receiverId,
    MessageNotificationResult message)
        {
            var userConnection =
                await userConnectionRepository
                    .GetByUserId(receiverId);

            if (userConnection == null)
                return;
            var connection = userConnection.FirstOrDefault();

            if (connection == null)
            {
                return;
            }

            await hubContext.Clients
                .Client(userConnection.FirstOrDefault().ConnectionId)
                .SendAsync(
                    "ReceiveMessage",
                    message);
        }
        public async Task NotifyMessageRead(
       int senderId,
       int messageId)
        {
            var userConnection =
                await userConnectionRepository
                    .GetByUserId(senderId);

            if (userConnection.Count == 0 )
                return;

            await hubContext.Clients
                .Client(userConnection.FirstOrDefault().ConnectionId)
                .SendAsync(
                    "MessageRead",
                    messageId);
        }


    }

 }

