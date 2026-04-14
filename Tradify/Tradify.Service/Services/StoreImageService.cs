using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;

namespace Tradify.Service.Services
{
    public class StoreImageService : Service<StoreImage>, IStoreImageService
    {
        public StoreImageService(IGenericRepository<StoreImage> repository) : base(repository)
        {
        }
    }
}
