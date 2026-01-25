using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Comments;

namespace Tradify.Infrastructure.Configurations
{
    public class ReplyOFCommentConfiguration : IEntityTypeConfiguration<ReplyOFComment>
    {
        public void Configure(EntityTypeBuilder<ReplyOFComment> builder)
        {
            

            // Relation with UserThatWriteAComment
            builder.HasOne(r => r.UserThatWriteAComment)
                   .WithMany()
                   .HasForeignKey(r => r.UserIdThatWriteAComment)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relation with UserThatWriteAReplyOFComment
            builder.HasOne(r => r.UserThatWriteAReplyOFComment)
                   .WithMany()
                   .HasForeignKey(r => r.UserIdThatWriteAReplyOFComment)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
