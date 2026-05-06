using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
namespace Tradify.Infrastructure.Seeder
{
    public class SellerSeeder
    {
        private static Sellers AddSeller(int userId, string businessName, string businessType)
        {
            return new Sellers
            {
                UserId = userId,
                BusinessName = businessName,
                BusinessType = businessType,
                IsActive = true

            };
        }

        public static async Task SeedAsync(ApplicationDbContext context)
        {
            var sellers = new List<Sellers>()
            {

                      AddSeller(2, "Trendy Wear", "Clothing Store"),
                      AddSeller (3, "Arms Flowers", "Flower Shop"),
                      AddSeller(4, "House Needs", "Home Supplies Store"),
                      AddSeller(5, "Fresh Market", "Supermarket"),
                      AddSeller(6, "Tech Hub", "Electronics Store"),
                      AddSeller(7, "Beauty Care", "Cosmetics & Skincare Store"),
                      AddSeller(8, "Health Care", "Pharmacy"),
                      AddSeller(9, "Life Care Clinic", "Medical Clinic"),
                      AddSeller(10, "Body Gym", "Fitness Center"),
                      AddSeller(11, "Smart Learn Center", "Educational Center"),
                      AddSeller(12, "Flash Studio", "Photography Studio"),
                      AddSeller(13, "Smile Care Clinic", "Dental Clinic")
            };


            foreach (var seller in sellers)
            {
                var existingSeller = await context.Sellers.FirstOrDefaultAsync(s => s.BusinessName == seller.BusinessName);

                if (existingSeller == null)
                {
                    await context.Sellers.AddAsync(seller);
                    await context.SaveChangesAsync();

                }
            }

        }
    } 
}
