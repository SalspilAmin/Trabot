using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Chat;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;

namespace Tradify.Service.Services
{
    public class MessageMediaPathService : Service<MessageMediaPath>, IMessageMediaPathService
    {
        public MessageMediaPathService(IGenericRepository<MessageMediaPath> repository) : base(repository)
        {
        }
    }
}
