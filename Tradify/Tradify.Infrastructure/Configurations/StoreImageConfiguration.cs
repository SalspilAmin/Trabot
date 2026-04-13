using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;

namespace Tradify.Infrastructure.Configurations
{
    public class StoreImageConfiguration : IEntityTypeConfiguration<StoreImage>
    {
        public void Configure(EntityTypeBuilder<StoreImage> builder)
        {
            builder.HasIndex(x => x.StoreId).IsUnique();
        }
    }
}
