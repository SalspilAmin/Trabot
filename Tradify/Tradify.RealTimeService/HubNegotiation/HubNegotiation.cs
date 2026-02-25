using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Helpers;
using Tradify.Infrastructure.AbstractsRepositories.UserConnectionRepositories;

namespace Tradify.RealTimeService.HubNegotiation
{
     public class HubNegotiation : Hub
    {
        private readonly ILogger logger;
        private readonly IUserConnectionRepository userConnection;

        public HubNegotiation(ILogger logger, IUserConnectionRepository userConnection)
        {
            this.logger = logger;
            this.userConnection = userConnection;
        }
        public  override async Task OnConnectedAsync()
        {
            using (var trans = await userConnection.BeginTransactionAsync())
            {
                try
                {
                    string ConnectionId = Context.ConnectionId;
                    int userId = int.Parse(Context.User.FindFirst(nameof(UserClaimModel.Id))?.Value);
                    // to test code will intialize this id for user
                    userId = 2;
                    await userConnection.AddAsync(new Data.Entities.UserConnection.UserConnection()
                    { 
                        ConnectionId = ConnectionId,
                        UserId = userId
                    
                    });
                    await userConnection.SaveChangesAsync();
                    await userConnection.CommitAsync(trans);
                    await base.OnConnectedAsync();



                }
                catch (Exception ex)
                {
                    await userConnection.RollBackAsync(trans);
                    logger.LogError(ex.Message);
                    throw ex;
                }
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            using (var trans = await userConnection.BeginTransactionAsync())
            {
                try
                {
                    string ConnectionId = Context.ConnectionId;
                    var UserConnection = await userConnection.GetByConnectionId(ConnectionId);

                    if(UserConnection is not null)
                    {
                        await userConnection.DeleteByConnectionId(ConnectionId);
                        await userConnection.SaveChangesAsync();
                        await userConnection.CommitAsync(trans);

                    }
                    else
                    {
                        logger.LogWarning("User Want To DisConnect But Is Not Found Is An Online User");
                        await userConnection.RollBackAsync(trans);

                    }

                    await base.OnDisconnectedAsync(exception);
                    
                }
                catch 
                {
                    await userConnection.RollBackAsync(trans);
                    logger.LogError(exception, "Error in OnDisconnectedAsync in TextPostHubService");
                    throw;
                }

            }
        }
    }
}
