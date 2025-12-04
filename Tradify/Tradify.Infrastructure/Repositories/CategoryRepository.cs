using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;

namespace Tradify.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Categories>, ICateforyRepository
    {
        #region Filds
        private  DbSet<Categories> categories;
        #endregion

        #region Constructor
        public CategoryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) 
        { 
            categories = applicationDbContext.Set<Categories>();
        }

        #endregion

        #region Methods

        #endregion


    }
}
