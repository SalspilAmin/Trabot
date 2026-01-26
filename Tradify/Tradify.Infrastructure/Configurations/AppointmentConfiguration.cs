using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;

namespace Tradify.Infrastructure.Configurations
{
    internal class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {          
            builder.HasOne(a => a.Customer)
                   .WithMany()
                   .HasForeignKey(a => a.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);

           
            builder.HasOne(a => a.Instructor)
                   .WithMany()
                   .HasForeignKey(a => a.InstructorId)
                   .OnDelete(DeleteBehavior.Restrict);

          
            builder.HasOne(a => a.StoreBooking)
                   .WithMany()
                   .HasForeignKey(a => a.StoreBookingId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
