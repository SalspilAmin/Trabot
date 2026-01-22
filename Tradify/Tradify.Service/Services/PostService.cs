using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Posts;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;

namespace Tradify.Service.Services
{
    public class PostService : Service<Post>, IPostService
    {
        public PostService(IGenericRepository<Post> repository) : base(repository)
        {
        }
    }
}
