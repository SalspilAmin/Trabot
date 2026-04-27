using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Appointments;

namespace Tradify.Infrastructure.Configurations
{
    public class CertificationsConfiguration : IEntityTypeConfiguration<Certifications>
    {
        public void Configure(EntityTypeBuilder<Certifications> builder)
        {

        }
    }
}
