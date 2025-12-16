using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;

namespace Tradify.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {

            builder.HasMany(x => x.ProductVariants)
                           .WithOne(x => x.Product)
                           .HasForeignKey(x => x.ProductId)
                           .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.ProductVideos)
                         .WithOne(x => x.Product)
                         .HasForeignKey(x => x.ProductId)
                         .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.ProductImages)
                         .WithOne(x => x.Product)
                         .HasForeignKey(x => x.ProductId)
                         .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
