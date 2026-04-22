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

        public async Task NotifyReceiverAboutPost(int UserId, Message message,List<MessageMediaPath?> media = null)
        {
            try
            {
//List<string> ConnectionIdOFUsersThatIsConnectedNow = await userConnectionRepository.GetTableNoTracking().Select(x => x.ConnectionId).ToListAsync();
                //if (ConnectionIdOFUsersThatIsConnectedNow is null)
                //{
                //    // No users are connected, so we don't need to send anything
                //    return;
                //}

                //if (hubContext.Clients is null)
                //{

                //    return;
                //}
//await hubContext.Clients.Clients(ConnectionIdOFUsersThatIsConnectedNow).SendAsync("ReceivePost", post);

            }
            catch (Exception ex)
            {
                throw;
            }


        }

    }
}
