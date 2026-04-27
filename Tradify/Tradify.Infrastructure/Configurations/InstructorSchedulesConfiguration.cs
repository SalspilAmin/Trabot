using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Data.Entities.Appointments;

namespace Tradify.Infrastructure.Configurations
{
    public class InstructorSchedulesConfiguration : IEntityTypeConfiguration<InstructorSchedules>
    {
        public void Configure(EntityTypeBuilder<InstructorSchedules> builder)
        {


            builder.HasMany(x => x.Bookings)
                           .WithOne(x => x.Schedule)
                           .HasForeignKey(x => x.ScheduleId)
                           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
