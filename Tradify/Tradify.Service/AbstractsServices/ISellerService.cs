using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Data.Entities.Identity;
using Tradify.Infrastructure.AbstractsRepositories;

namespace Tradify.Service.AbstractsServices
{
    public interface ISellerService : ISellerRepository

    {
        Task<(string, int?)> AddSellerAsync(Sellers seller);
        
    }
}

