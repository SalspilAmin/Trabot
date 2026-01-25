using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Posts;

namespace Tradify.Infrastructure.Configurations
{
    public class ImageOrVideoPathConfiguration : IEntityTypeConfiguration<ImageOrVideoPath>
    {
        public void Configure(EntityTypeBuilder<ImageOrVideoPath> builder)
        {
          
        }
    }
}
 