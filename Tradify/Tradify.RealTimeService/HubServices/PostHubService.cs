using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Entities.Posts;
using Tradify.Infrastructure.AbstractsRepositories.UserConnectionRepositories;
using Tradify.Infrastructure.Repositories.UserConnectionRepositories;
using Tradify.RealTimeService.HubNegotiation;

namespace Tradify.RealTimeService.HubServices
{
    public class PostHubService
    {
        private readonly IUserConnectionRepository userConnectionRepository;
        private readonly IHubContext<Tradify.RealTimeService.HubNegotiation.HubNegotiation> hubContext;

        public PostHubService(IUserConnectionRepository userConnection,IHubContext<Tradify.RealTimeService.HubNegotiation.HubNegotiation> hubcontext)
        {
            this.userConnectionRepository = userConnection;
            this.hubContext = hubcontext;
        }
      public async Task NotifyAllAboutPost(Post post)
        {
            try
            {
                List<string> ConnectionIdOFUsersThatIsConnectedNow = await userConnectionRepository.GetTableNoTracking().Select(x=>x.ConnectionId).ToListAsync();
                if (ConnectionIdOFUsersThatIsConnectedNow is null)
                {
                    // No users are connected, so we don't need to send anything
                    return;
                }
               
                if (hubContext.Clients is null)
                {
                 
                    return;
                }
                await hubContext.Clients.Clients(ConnectionIdOFUsersThatIsConnectedNow).SendAsync("ReceivePost", post);

            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
