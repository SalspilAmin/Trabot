using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;

namespace Tradify.Infrastructure.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payments>
    {
        public void Configure(EntityTypeBuilder<Payments> builder)
        {
           
        }
    }
}
