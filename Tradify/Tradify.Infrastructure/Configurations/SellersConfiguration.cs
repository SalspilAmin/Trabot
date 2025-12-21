using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;

namespace Tradify.Infrastructure.Configurations
{
    public class SellersConfiguration : IEntityTypeConfiguration<Sellers>
    {
        public void Configure(EntityTypeBuilder<Sellers> builder)
        {
            builder.HasOne(c => c.Store).WithOne(c => c.Seller)
                .HasForeignKey<Stores>(c => c.SellerId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
