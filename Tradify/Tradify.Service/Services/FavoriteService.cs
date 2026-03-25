using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;

namespace Tradify.Service.Services
{
    public  class FavoriteService : Service<Favorite>, IFavoriteService
    {
        public FavoriteService(IGenericRepository<Favorite> repository) : base(repository)
        { 
        }
    }
}
