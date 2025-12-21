using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Infrastructure.InfrastrucureBases;

namespace Tradify.Service.ServiceBases
{
    public interface IService<T> : IGenericRepository<T> where T : class
    {
    }
}
