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
    public class MessageMediaPathRepository : GenericRepository<MessageMediaPath>, IMessageMediaPathRepository
    {
        #region Filds
        private DbSet<MessageMediaPath> MessageMediaPaths;
        #endregion

        #region Constructor
        public MessageMediaPathRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            MessageMediaPaths = applicationDbContext.Set<MessageMediaPath>();
        }

        #endregion

        #region Methods

        #endregion
    }
}
