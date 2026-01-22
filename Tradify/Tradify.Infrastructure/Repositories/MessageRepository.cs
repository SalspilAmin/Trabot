using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Data.Entities.Chat;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;

namespace Tradify.Infrastructure.Repositories
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        #region Filds
        private DbSet<Message> Messages;
        #endregion

        #region Constructor
        public MessageRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            Messages = applicationDbContext.Set<Message>();
        }

        #endregion

        #region Methods

        #endregion
    }
}
