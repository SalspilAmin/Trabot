using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Data.Entities.Posts;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;

namespace Tradify.Infrastructure.Repositories
{
    public class ImageOrVideoPathRepository : GenericRepository<ImageOrVideoPath>, IImageOrVideoPathRepository
    {
        #region Filds
        private DbSet<ImageOrVideoPath> ImageOrVideoPaths;
        #endregion

        #region Constructor
       

 public ImageOrVideoPathRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            ImageOrVideoPaths = applicationDbContext.Set<ImageOrVideoPath>();
        
        }
        #endregion

        #region Methods

        #endregion
       
    }
}
