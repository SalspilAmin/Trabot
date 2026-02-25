using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Entities.UserConnection;
using Tradify.Infrastructure.AbstractsRepositories.UserConnectionRepositories;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;

namespace Tradify.Infrastructure.Repositories.UserConnectionRepositories
{
    public class UserConnectionRepository : GenericRepository<UserConnection>, IUserConnectionRepository
    {
        private readonly ApplicationDbContext Context;
        private readonly ILogger<UserConnection> logger;
        public UserConnectionRepository(ApplicationDbContext application, ILogger<UserConnection> logger) : base(application)      
        {
            Context = application;
            this.logger = logger;
        }
        public async Task DeleteByConnectionId(string ConnectionId)
        {

            try
            {
                UserConnection? UserConnectionThatWantToDelete = await GetByConnectionId(ConnectionId);
                if (UserConnectionThatWantToDelete is not null)
                {
                    Context.userConnections.Remove(UserConnectionThatWantToDelete);
                   Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task DeleteByUserId(int UserId)
        {
            try
            {

                List<UserConnection> AllUserConnectionOFUser = await GetByUserId(UserId);
                if (AllUserConnectionOFUser is not null && AllUserConnectionOFUser.Count > 0)
                {
                    Context.userConnections.RemoveRange(AllUserConnectionOFUser);
                    Context.SaveChanges();
                }
                return;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<UserConnection?> GetByConnectionId(string ConnectionId)
        {
            try
            {
                return await Context.userConnections.FirstOrDefaultAsync(x => x.ConnectionId == ConnectionId);

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<UserConnection>?> GetByUserId(int UserId)
        {
            try
            {
                return await Context.userConnections.Where(x=>x.UserId == UserId).ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> UserIsOnlineOrNot(int UserId)
        {
            try
            {


                List<UserConnection> UserConnectionOFUser = await GetByUserId(UserId);
                return UserConnectionOFUser is not null && UserConnectionOFUser.Count > 0;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
