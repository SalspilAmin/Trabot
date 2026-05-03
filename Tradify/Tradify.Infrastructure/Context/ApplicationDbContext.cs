using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Data.Entities.Appointments;
using Tradify.Data.Entities.Chat;
using Tradify.Data.Entities.Comments;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Entities.Posts;
using Tradify.Data.Entities.UserConnection;
using Tradify.Infrastructure.Configurations;

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
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<Payouts>  Payouts { get; set; }
        public DbSet<ProductVariants> ProductVariants { get; set; }

        public DbSet<ProductVideo> ProductVideo { get; set; }

        public DbSet<ProductImage> ProductImage { get; set; }

        public DbSet<ProductVariantImage> ProductVariantImages { get; set; }


        public DbSet<Products> Products { get; set; }

        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        public DbSet<Shipments> Shipments { get; set; }
        public DbSet<ShipmentTracking > ShipmentTracking { get; set; }

        public DbSet<Stores> Stores { get; set; }

        public DbSet<SubOrders> SubOrders { get; set; }


        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageMediaPath> MessageMediaPaths { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ReplyOFComment> ReplyOFComments { get; set; }
        public DbSet<ImageOrVideoPath> ImageOrVideoPaths { get; set; }
        public DbSet<InteractionWithPost> InteractionWithPosts { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }   
        public  DbSet<UserConnection>  userConnections { get; set; }    

        public DbSet<Sellers> Sellers { get; set; } 

        public DbSet<StoreImage> StoreImages { get; set; }  

        public DbSet<Bookings> Bookings { get; set; }
        public DbSet<Certifications> Certifications { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<InstructorImage> InstructorImage { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<Instructors> Instructors { get; set; }
        public DbSet<InstructorSchedules> InstructorSchedules { get; set; } 


        #endregion

        #region Constructors




        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            encryptionProvider = new GenerateEncryptionProvider("ysupersecretkey123456789012345@#");
        }

        

        #endregion


        #region Methods


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.UseEncryption(encryptionProvider);
            modelBuilder.Entity<Stores>().HasQueryFilter(s => !s.IsDeleted);
            modelBuilder.Entity<ProductVariants>().HasQueryFilter(v => !v.IsDeleted &&!v.Product.IsDeleted);
            modelBuilder.Entity<Products>().HasQueryFilter(p => !p.IsDeleted && !p.Store.IsDeleted);
            modelBuilder.Entity<Categories>().HasQueryFilter(s => !s.IsDeleted);
            modelBuilder.Entity<Instructors>().HasQueryFilter(s => s.IsActive);



        }


        #endregion




    }
}
