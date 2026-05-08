using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Chat;
using Tradify.Data.Entities.Posts;
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

        public async Task NotifyReceiverAboutPost(int UserId, Message message,MessageMediaPath? media = null)
        {
            try
            {
                // get userConnection
                var ReciverUserConnection = await userConnectionRepository.GetByIdAsync(message.ReceiverId);

                if (ReciverUserConnection is null)
                {
                    // No users are connected, so we don't need to send anything
                    return;
                }

                if (hubContext.Clients is null)
                {

                    return;
                }

                await hubContext.Clients.Client(ReciverUserConnection.ConnectionId).SendAsync("ReceivePost",message,media  );

            }
            catch (Exception ex)
            {
                throw;
            }


        }

    }
}
