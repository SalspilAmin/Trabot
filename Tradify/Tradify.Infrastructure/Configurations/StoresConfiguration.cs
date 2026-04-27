using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;

namespace Tradify.Infrastructure.Configurations
{
    public class StoresConfiguration : IEntityTypeConfiguration<Stores>
    {
        public void Configure(EntityTypeBuilder<Stores> builder)
        {
            builder.Property(x => x.Type)
                   .HasConversion<byte>()
                   .HasColumnType("tinyint");

            builder.HasOne(x => x.StoreImage)
                       .WithOne(x => x.Stores)
                       .HasForeignKey<StoreImage>(x => x.StoreId)
                       .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Products)
                           .WithOne(x => x.Store)
                           .HasForeignKey(x => x.StoreId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Categories)
                         .WithOne(x => x.Store)
                         .HasForeignKey(x => x.StoreId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Instructors)
                           .WithOne(x => x.Store)
                           .HasForeignKey(x => x.StoreId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Bookings)
                           .WithOne(x => x.Store)
                           .HasForeignKey(x => x.StoreId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
