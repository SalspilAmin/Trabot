using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;

namespace Tradify.Infrastructure.Configurations
{
    public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariants>
    {
        public void Configure(EntityTypeBuilder<ProductVariants> builder)
        {
          
        }
    }
}
