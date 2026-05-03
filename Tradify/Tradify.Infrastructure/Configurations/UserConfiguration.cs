using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Data.Entities.Identity;

namespace Tradify.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder.HasMany(x => x.Orders).WithOne(x => x.User).HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x=>x.Bookings).WithOne(x=>x.Customer).HasForeignKey(x=>x.CustomerId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Cart).WithOne(x => x.User).HasForeignKey<Cart>(c => c.UserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
