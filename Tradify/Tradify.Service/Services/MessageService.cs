using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Chat;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;

namespace Tradify.Service.Services
{
    public class MessageService : Service<Message>, IMessageService
    {
        private readonly ApplicationDbContext context;

        public MessageService(IGenericRepository<Message> repository,ApplicationDbContext context) : base(repository)
        {
           this.context = context;
        }

        public async Task<List<Message>> GetConversationAsync(int senderId, int receiverId)
        {
            return await context.Messages

            .Include(x => x.SenderUser)

            .Include(x => x.ReceiverUser)

           .Include(x => x.MessageMediaPaths)

           .Where(x =>

           (x.SenderId == senderId
           && x.ReceiverId == receiverId)

           ||

           (x.SenderId == receiverId
           && x.ReceiverId == senderId))

           .Where(x => !x.IsDeleted)

             .OrderBy(x => x.CreatedAt)

            .ToListAsync();
        }

        public async Task<List<Message>> GetUnreadMessagesAsync(int userId)
        {
            return await context.Messages

        .Include(x => x.SenderUser)

        .Include(x => x.ReceiverUser)

        .Include(x => x.MessageMediaPaths)

        .Where(x =>
            x.ReceiverId == userId
            &&
            !x.IsRead
            &&
            !x.IsDeleted)

        .OrderByDescending(x => x.CreatedAt)

        .ToListAsync();
        }
    }
}
