using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
namespace Tradify.Infrastructure.Seeder
{
    public class CategorySeeder
    {
        private static Categories AddCategory(string name, int storeId)
        {
            return new Categories
            {
                Name = name,
                StoreId = storeId,
                ParentCategoryId = null,
                IsDeleted = false
            };
        }

        public static async Task SeedAsync(ApplicationDbContext context)
        {
            var categories = new List<Categories>()
            {
                // 🔹 Trendy Wear (StoreId = 1)
                AddCategory("Men Clothing", 1),
                AddCategory("Women Clothing", 1),
                AddCategory("Kids Wear", 1),
                AddCategory("Shoes", 1),

                // 🔹 Arms Flowers (StoreId = 2)
                AddCategory("Bouquets", 2),
                AddCategory("Wedding Flowers", 2),
                AddCategory("HandMade Bouquets", 2),
                AddCategory("Accessories", 2),

                // 🔹 House Needs (StoreId = 3)
                AddCategory("Furniture", 3),
                AddCategory("Home Decor", 3),
                AddCategory("Kitchen Tools", 3),
                AddCategory("Electronics", 3),
                AddCategory("Cleaning Supplies", 3),

                // 🔹 Fresh Market (StoreId = 4)
                AddCategory("Fruits ", 4),
                AddCategory("Vegetables", 4),
                AddCategory("Dairy Products", 4),
                AddCategory("Meat & Poultry", 4),
                AddCategory("Snacks", 4),
                AddCategory("Drinks", 4),

                // 🔹 Tech Hub (StoreId = 5)
                AddCategory("Mobile Phones", 5),
                AddCategory("Laptops", 5),
                AddCategory("Accessories", 5),
                AddCategory("Smart Devices", 5),

                // 🔹 Beauty Care (StoreId = 6)
                AddCategory("Makeup", 6),
                AddCategory("Skincare", 6),
                AddCategory("Hair Care", 6),
                AddCategory("Perfume", 6),
                AddCategory("Personal Care", 6),

                // 🔹 Health Care (StoreId = 7)
                AddCategory("Medicines", 7),
                AddCategory("Vitamins ", 7),
                AddCategory("Medical Supplies", 7),
                AddCategory("Baby Care", 7),
            };

            foreach (var category in categories)
            {
                var exists = await context.Categories
                    .FirstOrDefaultAsync(c => c.Name == category.Name && c.StoreId == category.StoreId);

                if (exists == null)
                {
                    await context.Categories.AddAsync(category);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}

