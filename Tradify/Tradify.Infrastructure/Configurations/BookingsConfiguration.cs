using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Appointments;

namespace Tradify.Infrastructure.Configurations
{
    public class BookingsConfiguration : IEntityTypeConfiguration<Bookings>
    {
        public void Configure(EntityTypeBuilder<Bookings> builder)
        {

        }
    }
    }
