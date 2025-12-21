using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Data.Entities.Identity;

namespace Tradify.Infrastructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<User,Role,int>
    {

        #region Fields

        private readonly IEncryptionProvider encryptionProvider;

        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<UserRefreshToken> UserRefreshToken { get; set; }
        public DbSet<Categories> Categories { get;  set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<Payouts>  Payouts { get; set; }
        public DbSet<ProductVariants> ProductVariants { get; set; }

        public DbSet<ProductVideo> ProductVideo { get; set; }

        public DbSet<ProductImage> ProductImage { get; set; }  

        public DbSet<Products> Products { get; set; }

        public DbSet<Reviews> Reviews { get; set; } 

        public DbSet<Shipments> Shipments { get; set; }
        public DbSet<ShipmentTracking > ShipmentTracking { get; set; }

        public DbSet<Stores> Stores { get; set; }

        public DbSet<SubOrders> SubOrders { get; set; } 
        
      

        #endregion

        #region Constructors


       

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            encryptionProvider = new GenerateEncryptionProvider("HS12@#$");
        }

        

        #endregion


        #region Methods


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.UseEncryption(encryptionProvider);
                
        }


        #endregion

               


    }
}
