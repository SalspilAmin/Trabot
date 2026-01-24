using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Posts;

namespace Tradify.Infrastructure.Configurations
{
    public class InteractionWithPostConfiguration : IEntityTypeConfiguration<InteractionWithPost>
    {
        public void Configure(EntityTypeBuilder<InteractionWithPost> builder)
        {
            builder.HasOne(i => i.User)
                   .WithMany()
                   .HasForeignKey(i => i.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

           
        }
    }
}
