using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Data.Enums;
using Tradify.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Tradify.Infrastructure.Seeder
{
    public class StoreSeeder
    {
        private static Stores AddStore(int sellerId, string name, string description , StoreType type)
        {
            return new Stores
            {
                SellerId = sellerId,
                Name = name,
                Description = description,
                Type = type,
                IsActive = true,
                IsDeleted=false

            };
        }

        public static async Task SeedAsync(ApplicationDbContext context)
        {
            var stores = new List<Stores>()
{
    AddStore(1, "Trendy Wear",
        "Discover the latest fashion trends with stylish outfits for every occasion. High-quality clothing designed to keep you confident and comfortable.",
        StoreType.Product),

    AddStore(2, "Arms Flowers",
        "Fresh flowers and elegant bouquets crafted for every special moment. We bring beauty and fragrance to your celebrations with premium floral designs.",
        StoreType.Product),

    AddStore(3, "House Needs",
        "Everything your home needs in one place, from essentials to modern decor. Practical, stylish, and affordable products for everyday living.",
        StoreType.Product),

    AddStore(4, "Fresh Market",
        "Your go-to supermarket for fresh groceries, daily essentials, and high-quality products. Enjoy a convenient shopping experience with a wide variety of choices.",
        StoreType.Product),

    AddStore(5, "Tech Hub",
        "Explore the latest electronics, gadgets, and smart devices. We offer high-quality technology solutions to keep you connected and up to date.",
        StoreType.Product),

    AddStore(6, "Beauty Care",
        "Premium cosmetics and skincare products to enhance your natural beauty. Discover trusted brands and effective solutions for every skin type.",
        StoreType.Product),

    AddStore(7, "Health Care",
        "Your trusted pharmacy providing medications, healthcare products, and expert advice. We are committed to supporting your health and well-being.",
        StoreType.Product),

    AddStore(8, "Life Care Clinic",
        "Professional medical services with experienced doctors dedicated to your health. We provide reliable care in a comfortable and safe environment.",
        StoreType.Service),

    AddStore(9, "Body Gym",
"A modern fitness center equipped with state-of-the-art equipment to help you achieve your fitness goals. Train with professionals in a motivating and energetic environment.",       
        StoreType.Service),

    AddStore(10, "Smart Learn Center",
        "An innovative educational center offering high-quality learning programs. We help students develop skills and achieve academic excellence.",
        StoreType.Service),

    AddStore(11, "Flash Studio",
        "Capture your special moments with professional photography services. We deliver high-quality images with creativity and attention to detail.",
        StoreType.Service),

    AddStore(12, "Smile Care Clinic",
        "Comprehensive dental care services to keep your smile healthy and bright. Our experienced team ensures comfort and top-quality treatment.",
        StoreType.Service)
};


            foreach (var store in stores)
            {
                var existingStore = await context.Stores.FirstOrDefaultAsync(s => s.Name == store.Name);

                if (existingStore == null)
                {
                    await context.Stores.AddAsync(store);
                    await context.SaveChangesAsync();

                }
            }

        }
    }
}
