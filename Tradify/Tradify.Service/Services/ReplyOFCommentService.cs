using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Chat;
using Tradify.Data.Entities.Comments;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;

namespace Tradify.Service.Services
{
    public class ReplyOFCommentService : Service<ReplyOFComment>, IReplyOFCommentService
    {
        public ReplyOFCommentService(IGenericRepository<ReplyOFComment> repository) : base(repository)
        {
        }
    }
}
