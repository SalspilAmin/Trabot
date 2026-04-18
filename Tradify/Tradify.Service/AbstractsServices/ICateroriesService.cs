using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.AbstractsRepositories;

namespace Tradify.Service.AbstractsServices
{
    public interface ICateroriesService : ICategoryRepository
    {
        public Task<(string, int?)> AddCategoriesAsync(Categories categories);//,int sellerId);

        public Task<string> UpdateCategory(Categories categories);//, int sellerId);
    }
}
