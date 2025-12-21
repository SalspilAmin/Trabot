using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using Tradify.Data.Entities;

namespace Tradify.Infrastructure.Configurations
{
    //builder.HasMany(x => x.Students)
    //               .WithOne(x => x.Department)
    //               .HasForeignKey(x => x.DID)
    //               .OnDelete(DeleteBehavior.Restrict);

    //builder.HasOne(ds => ds.Department)
    //         .WithMany(d => d.DepartmentSubjects)
    //         .HasForeignKey(ds => ds.DID);
    public class CategoryConfiguration : IEntityTypeConfiguration<Categories>
    {
        public void Configure(EntityTypeBuilder<Categories> builder)
        {

            builder.HasMany(x => x.Products)
                           .WithOne(x => x.Category)
                           .HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);

            builder
             .HasOne(c => c.Parent)
                     .WithMany(c => c.Children)
                         .HasForeignKey(c => c.ParentCategoryId)
                               .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
