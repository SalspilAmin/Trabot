using Microsoft.AspNetCore.Mvc;
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


                await hubContext.Clients.All.SendAsync("ReceivePost", post);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
            #region Comments

        public async Task NotifyAddComment(CommentResult comment)
        {
            await hubContext.Clients.All
                .SendAsync("ReceiveComment", comment);
        }

        public async Task NotifyDeleteComment(int commentId)
        {
            await hubContext.Clients.All
                .SendAsync("DeleteComment", commentId);
        }

        #endregion

        #region Replies

        public async Task NotifyAddReply(ReplyCommentResult reply)
        {
            await hubContext.Clients.All
                .SendAsync("ReceiveReply", reply);
        }

        #endregion

        #region Interactions

        public async Task NotifyAddInteraction(PostInteractionResult interaction)
        {
            await hubContext.Clients.All
                .SendAsync("ReceiveInteraction", interaction);
        }

        public async Task NotifyRemoveInteraction(int interactionId)
        {
            await hubContext.Clients.All
                .SendAsync("RemoveInteraction", interactionId);
        }

        #endregion
    }

}

