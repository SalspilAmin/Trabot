using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Chat;

namespace Tradify.Infrastructure.Configurations
{
    public class MessageMediaPathConfiguration : IEntityTypeConfiguration<MessageMediaPath>
    {
        public void Configure(EntityTypeBuilder<MessageMediaPath> builder)
        {
            
        }
    }
}
 