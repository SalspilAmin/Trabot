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

            builder.HasMany(x => x.Products)
                           .WithOne(x => x.Store)
                           .HasForeignKey(x => x.StoreId).OnDelete(DeleteBehavior.Restrict);



        }
    }
}
