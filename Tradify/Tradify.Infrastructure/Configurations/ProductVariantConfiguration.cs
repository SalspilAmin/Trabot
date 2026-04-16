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
            //builder.HasMany(x => x.ProductVariantImages)
            //               .WithOne(x => x.ProductVariant)
            //               .HasForeignKey(x => x.ProductVariantId)
            //               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ProductVariantImage)
                       .WithOne(x => x.ProductVariant)
                       .HasForeignKey<ProductVariantImage>(x => x.ProductVariantId)
                       .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.OrderItems)
                          .WithOne(x => x.ProductVariant)
                          .HasForeignKey(x => x.ProductVariantId)
                          .OnDelete(DeleteBehavior.Restrict);
           
            // Computed Column
            builder.Property(v => v.FinalPrice)
                   .HasColumnType("decimal(18,2)")
                   .HasComputedColumnSql("[Price] - ([Price] * ([Discount] / 100))");
        }
    }
}
