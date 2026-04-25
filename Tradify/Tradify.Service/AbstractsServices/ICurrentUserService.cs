using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Service.Services;

namespace Tradify.Service.AbstractsServices
{
    public interface ICurrentUserService 
    {
        int GetUserId();
        public  Task<SellerContextResult> GetValidSellerContextAsync();
    }
}
