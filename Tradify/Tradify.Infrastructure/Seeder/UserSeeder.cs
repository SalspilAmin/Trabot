using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Identity;

namespace Tradify.Infrastructure.Seeder
{
     public static class UserSeeder
    {
        
        
        public static async Task SeedAsync(UserManager<User> _userManager, IConfiguration configuration)
        {
            var usersCount = await _userManager.Users.CountAsync();
            if (usersCount <= 0)
            {
                var adminSettings = configuration.GetSection("AdminSettings");
                var defaultuser = new User()
                {
                    UserName = "Trabotadmin",
                    Email = adminSettings["Gmail"],
                    Country = adminSettings["Country"],
                    PhoneNumber = adminSettings["PhoneNumber"],
                    Address = adminSettings["Address"],
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                await _userManager.CreateAsync(defaultuser, "M123_m@3%");
                await _userManager.AddToRoleAsync(defaultuser, "Admin");
            }
        }
    }
}
