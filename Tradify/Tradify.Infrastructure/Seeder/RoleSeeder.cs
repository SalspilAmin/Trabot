using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Identity;

namespace Tradify.Infrastructure.Seeder
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<Role> roleManager)
        {

            var count = await roleManager.Roles.CountAsync();

            if (count == 0)
            {

              await roleManager.CreateAsync(new Role()
                {
                    Name = "Admin"
                });
                await roleManager.CreateAsync(new Role()
                {
                    Name = "User"
                });
                 await roleManager.CreateAsync(new Role()
                {
                    Name = "Seller"
                });
                await roleManager.CreateAsync(new Role()
                {
                    Name = "Instructor"
                });
            }
        }
    }
}
