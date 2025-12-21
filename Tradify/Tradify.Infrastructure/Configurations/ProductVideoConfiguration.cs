using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;

namespace Tradify.Infrastructure.Configurations
{
    public class ProductVideoConfiguration : IEntityTypeConfiguration<ProductVideo>
    {
        public void Configure(EntityTypeBuilder<ProductVideo> builder)
        {
            
        }
    }
}
