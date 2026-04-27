using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Data.Entities.Appointments;

namespace Tradify.Infrastructure.Configurations
{
    public class InstructorsConfiguration : IEntityTypeConfiguration<Instructors>
    {
        public void Configure(EntityTypeBuilder<Instructors> builder)
        {
            builder.HasOne(x => x.InstructorImage)
                       .WithOne(x => x.Instructor)
                       .HasForeignKey<InstructorImage>(x => x.InstructorId)
                       .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Reviews)
                           .WithOne(x => x.Instructor)
                           .HasForeignKey(x => x.InstructorId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Educations)
                         .WithOne(x => x.Instructor)
                         .HasForeignKey(x => x.InstructorId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Certifications)
                           .WithOne(x => x.Instructor)
                           .HasForeignKey(x => x.InstructorId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Services)
                          .WithOne(x => x.Instructor)
                          .HasForeignKey(x => x.InstructorId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Schedules)
                         .WithOne(x => x.Instructor)
                         .HasForeignKey(x => x.InstructorId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
