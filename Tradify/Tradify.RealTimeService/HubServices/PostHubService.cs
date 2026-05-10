using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Entities.Posts;
using Tradify.Data.Helpers.Results;
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
      public async Task NotifyAllAboutPost(PostResult post)
        {
            try
            {
                if (hubContext.Clients == null) 
                    return;

                // السطر التالي يغنيك عن استدعاء قاعدة البيانات تماماً!
                // سيرسل المنشور لكل شخص فاتح صفحة الـ HTML حالياً
                await hubContext.Clients.All.SendAsync("ReceivePost", post);

            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
