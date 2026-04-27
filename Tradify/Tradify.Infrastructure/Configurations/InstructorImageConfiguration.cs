using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Appointments;

namespace Tradify.Infrastructure.Configurations
{
    public class InstructorImageConfiguration : IEntityTypeConfiguration<InstructorImage>
    {
        public void Configure(EntityTypeBuilder<InstructorImage> builder)
        {
            builder.HasIndex(x => x.InstructorId).IsUnique();
        }
    }
}
