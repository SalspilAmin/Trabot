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
            await hubContext.Clients
                .Group($"User-{receiverId}")
                .SendAsync(
                    "ReceiveMessage",
                    message);
        }

        public async Task NotifyMessageRead(
            int senderId,
            int messageId)
        {
            await hubContext.Clients
                .Group($"User-{senderId}")
                .SendAsync(
                    "MessageRead",
                    messageId);
        }


    }

 }

