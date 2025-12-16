using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;

namespace Tradify.Infrastructure.Configurations
{
    public class SubOrderConfiguration : IEntityTypeConfiguration<SubOrders>
    {
        public void Configure(EntityTypeBuilder<SubOrders> builder)
        {
            builder.HasMany(x => x.Products)
                           .WithOne(x => x.SubOrder)
                           .HasForeignKey(x => x.SuborderId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
