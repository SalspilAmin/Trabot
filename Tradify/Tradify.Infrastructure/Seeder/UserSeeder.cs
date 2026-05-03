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
        private static User CreateUser(string username, string email, string phone)
        {
            return new User
            {
                UserName = username,
                Email = email,
                Country = "Egypt",
                PhoneNumber = phone,
                Address = "Cairo",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
        }

        public static async Task SeedAsync(UserManager<User> _userManager, IConfiguration configuration)
        {
            // Admin
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

            // User For Seller List 
            var sellers = new List<User>()
            {
                // Product Stores
                   CreateUser("TrendyWearSeller2", "trendywear@gmail.com", "01160050092"),//Clothing                       UserId(5)
                   CreateUser("ArmsFlowersSeller3", "armesflowers@gmail.com", "01282623214"),//Flower                      UserId(6)   
                   CreateUser("HouseNeedsSeller4", "houseneeds@gmail.com", "01160550892"),//Home Supplies                  UserId(7)
                   CreateUser("FreshMarketSeller5", "freshmarket@gmail.com", "01160853092"),//Supermarket                  UserId(8)
                   CreateUser("TechHubSeller6", "techhub@gmail.com", "01260056692"),//Electronics Store (Phone)            UserId(9)
                   CreateUser("BeautyCareSeller7", "beautycare@gmail.com", "01560530092"),//Care Store                     UserId(10)
                   CreateUser("HealthCareSeller1", "healthcare@gmail.com", "01000000001"),//Pharmacy                       UserId(11)


              // Service Stores
                  CreateUser("LifeCareClinicSeller8", "lifecareclinic@gmail.com", "01060550092"),//Clinic                     UserId(12)
                  CreateUser("BodyGymSeller9", "bodygym@gmail.com", "01160050092"),//Gym                                      UserId(13)
                  CreateUser("SmartLearnCenterSeller10", "smartlearncenter@gmail.com", "01160080092"),//Education Center      UserId(14)
                  CreateUser("FlashStudioSeller11", "flashstudio@gmail.com", "01180056092"),// Photography Studio             UserId(15)
                  CreateUser("SmileCareClinicSeller12", "smilecareclinic@gmail.com", "01181056092")//Dental Clinic           UserId(16)
            };


            foreach (var seller in sellers)
            {
                var existingSeller = await _userManager.FindByEmailAsync(seller.Email);

                if (existingSeller == null)
                {
                    await _userManager.CreateAsync(seller, "Seller@2026");
                    await _userManager.AddToRoleAsync(seller, "Seller");
                }
            }




            // User For Instructor List  For Life Care Clinic


            var instructorsLifeCareClinic = new List<User>()
            {
                // Product Stores
                   CreateUser("DrAhmedHassan", "drahmedhassan@gmail.com", "01160850092"),//1   UserId(23)
                   CreateUser("DrSaraMohamed", "drsaramohamed@gmail.com", "01282623214"),//2   UserId(24)
                   CreateUser("DrMostafaAli", "drmostafaali@gmail.com", "01168550892"),//3     UserId(25)
                   CreateUser("DrNadaIbrahim", "drnadaibrahim@gmail.com", "01160953092"),//4   UserId(26)
                   CreateUser("DrOmarKhaled", "dromarkhaled@gmail.com", "01260356692"),//5     UserId(27)
                   CreateUser("DrMariamAdel", "drmariamadel@gmail.com", "01560530092"),//6     UserId(28)
                   CreateUser("DrYoussefSamir", "dryoussefsamir@gmail.com", "01000000801"),//7 UserId(29)
                   CreateUser("DrReemTarek", "drreemtarek@gmail.com", "01060550092"),//8       UserId(30)
                   CreateUser("DrHanyFathy", "drhanyfathy@gmail.com", "01160050092"),//9       UserId(31)
                   CreateUser("DrSalmaNabil", "drsalmanabil@gmail.com", "01160080092"),//10    UserId(32)
                 
            };


            foreach (var instructors in instructorsLifeCareClinic)
            {
                var existingInstructor = await _userManager.FindByEmailAsync(instructors.Email);

                if (existingInstructor == null)
                {
                    await _userManager.CreateAsync(instructors, "Instructor@2026");
                    await _userManager.AddToRoleAsync(instructors, "Instructor");
                }
            }


        }
    }
}
