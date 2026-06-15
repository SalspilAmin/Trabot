using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Chat;
using Tradify.Infrastructure.AbstractsRepositories;

namespace Tradify.Service.AbstractsServices
{
    public interface IMessageService : IMessageRepository
    {
        Task<List<Message>>
GetConversationAsync(
    int senderId,
    int receiverId);

        Task<List<Message>>
        GetUnreadMessagesAsync(
            int userId);
    }
}
