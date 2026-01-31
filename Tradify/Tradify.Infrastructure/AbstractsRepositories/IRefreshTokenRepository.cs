using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Identity;
using Tradify.Infrastructure.InfrastrucureBases;

namespace Tradify.Infrastructure.AbstractsRepositories
{
    public interface IRefreshTokenRepository : IGenericRepository<UserRefreshToken>
    {
    }
}
