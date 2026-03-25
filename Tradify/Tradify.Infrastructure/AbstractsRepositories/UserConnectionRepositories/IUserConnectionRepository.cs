using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.UserConnection;
using Tradify.Infrastructure.InfrastrucureBases;

namespace Tradify.Infrastructure.AbstractsRepositories.UserConnectionRepositories
{
     public  interface IUserConnectionRepository : IGenericRepository<UserConnection>
    {
        Task DeleteByConnectionId(string ConnectionId);
        Task DeleteByUserId(int UserId);
        Task<UserConnection?> GetByConnectionId(string ConnectionId);
        Task<List<UserConnection>?> GetByUserId(int UserId);
        Task<bool> UserIsOnlineOrNot(int UserId);
    }
}
