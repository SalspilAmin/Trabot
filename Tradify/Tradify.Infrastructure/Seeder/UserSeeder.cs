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


            var instructorsUsers = new List<User>()
            {
            // User For Instructor List  For Life Care Clinic

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


                  // User For Instructor List  For Body Gym

                   CreateUser("CaptainAhmedSamy", "captainahmedsamy@gmail.com", "01160850092"),//1   UserId(41)
                   CreateUser("CaptainNourAli", "captainnourali@gmail.com", "01282623214"),//2   UserId(42)
                   CreateUser("CaptainMostafaAdel", "captainmostafaadel@gmail.com", "01168550892"),//3     UserId(43)
                   CreateUser("CaptainOmarKhaled", "captainomarkhaled@gmail.com", "01160953092"),//4   UserId(44)
                   CreateUser("CaptainYoussefNabil", "captainyoussefnabil@gmail.com", "01260356692"),//5     UserId(45)
                  
                 
                  // User For Instructor List  For Smart Learn Center

                   CreateUser("MrAhmedHassan", "mrahmedhassan@gmail.com", "01160850092"),//1   UserId(56)
                   CreateUser("MsSaraMohamed", "mssaramohamed@gmail.com", "01282623214"),//2   UserId(57)
                   CreateUser("MrMostafaAli", "mrmostafaali@gmail.com", "01168550892"),//3     UserId(58)
                   CreateUser("MsNadaIbrahim", "msnadaibrahim@gmail.com", "01160953092"),//4   UserId(59)
                   CreateUser("MrOmarKhaled", "mromarkhaled@gmail.com", "01260356692"),//5     UserId(60)
                   CreateUser("MsMariamAdel", "msmariamadel@gmail.com", "01282623214"),//2   UserId(61)
                   CreateUser("MrYoussefSamir", "mryoussefsamir@gmail.com", "01160850092"),//1   UserId(62)
                   CreateUser("MsReemTarek", "msreemtarek@gmail.com", "01168550892"),//3     UserId(63)
                   CreateUser("MrHanyFathy", "mrhanyfathy@gmail.com", "01160953092"),//4   UserId(64)
                   CreateUser("MsSalmaNabil", "mssalmanabil@gmail.com", "01260356692"),//5     UserId(65)

                  // User For Instructor List  For  Flash Studio

                   CreateUser("AhmedSamy", "ahmedsamy@gmail.com", "01160850092"),//1   UserId(66)
                   CreateUser("SaraAdel", "saraadel@gmail.com", "01168550892"),//3     UserId(67)
                   CreateUser("MostafaNabil", "mostafanabil@gmail.com", "01160953092"),//4   UserId(68)
                   CreateUser("ReemTarek", "reemtarek@gmail.com", "01260356692"),//5     UserId(69)

                  // User For Instructor List  For  Smile Care Clinic
                  
                  CreateUser("DrKareemElSayed", "drkareemelsayed@gmail.com", "01160850092"),//1   UserId(70)
                   CreateUser("DrLailaFarouk", "drlailafarouk@gmail.com", "01168550892"),//3     UserId(71)
                   CreateUser("DrHossamAbdelrahman", "drhossamabdelrahman@gmail.com", "01160953092"),//4   UserId(72)
            };


            foreach (var instructors in instructorsUsers)
            {
                var existingInstructor = await _userManager.FindByEmailAsync(instructors.Email);

                if (existingInstructor == null)
                {
                    await _userManager.CreateAsync(instructors, "Instructor@2026");
                    await _userManager.AddToRoleAsync(instructors, "Instructor");
                }
            }




            var ClinetUsers = new List<User>()
            {
            CreateUser("AhmedSamy1", "ahmedsamy1@gmail.com", "01010000001"),
    CreateUser("SaraAli2", "saraali2@gmail.com", "01010000002"),
    CreateUser("MostafaHassan3", "mostafahassan3@gmail.com", "01010000003"),
    CreateUser("NadaIbrahim4", "nadaibrahim4@gmail.com", "01010000004"),
    CreateUser("OmarKhaled5", "omarkhaled5@gmail.com", "01010000005"),

    CreateUser("MariamAdel6", "mariamadel6@gmail.com", "01010000006"),
    CreateUser("YoussefNabil7", "youssefnabil7@gmail.com", "01010000007"),
    CreateUser("ReemTarek8", "reemtarek8@gmail.com", "01010000008"),
    CreateUser("HanyFathy9", "hanyfathy9@gmail.com", "01010000009"),
    CreateUser("SalmaNabil10", "salmanabil10@gmail.com", "01010000010"),

    CreateUser("KarimAdel11", "karimadel11@gmail.com", "01010000011"),
    CreateUser("LailaFarouk12", "lailafarouk12@gmail.com", "01010000012"),
    CreateUser("HossamAli13", "hossamali13@gmail.com", "01010000013"),
    CreateUser("NourHassan14", "nourhassan14@gmail.com", "01010000014"),
    CreateUser("TarekSamir15", "tareksamir15@gmail.com", "01010000015"),

    CreateUser("AyaMostafa16", "ayamostafa16@gmail.com", "01010000016"),
    CreateUser("MahmoudYasser17", "mahmoudyasser17@gmail.com", "01010000017"),
    CreateUser("FarahAdel18", "farahadel18@gmail.com", "01010000018"),
    CreateUser("ZiadNabil19", "ziadnabil19@gmail.com", "01010000019"),
    CreateUser("MennaWael20", "mennawael20@gmail.com", "01010000020"),

    CreateUser("AmrEssam21", "amressam21@gmail.com", "01010000021"),
    CreateUser("HabibaAli22", "habibaali22@gmail.com", "01010000022"),
    CreateUser("KhaledTamer23", "khaledtamer23@gmail.com", "01010000023"),
    CreateUser("YaraAhmed24", "yaraahmed24@gmail.com", "01010000024"),
    CreateUser("AdhamSherif25", "adhamsherif25@gmail.com", "01010000025"),

    CreateUser("RanaMagdy26", "ranamagdy26@gmail.com", "01010000026"),
    CreateUser("MinaNader27", "minanader27@gmail.com", "01010000027"),
    CreateUser("DinaWael28", "dinawael28@gmail.com", "01010000028"),
    CreateUser("SeifIslam29", "seifislam29@gmail.com", "01010000029"),
    CreateUser("MalakHesham30", "malakhesham30@gmail.com", "01010000030"),
    CreateUser("OlaKamal31", "olakamal31@gmail.com", "01010000031"),

CreateUser("MohamedAdel32", "mohamedadel32@gmail.com", "01010000032"),

CreateUser("NadineAshraf33", "nadineashraf33@gmail.com", "01010000033"),
CreateUser("SherifWael34", "sherifwael34@gmail.com", "01010000034"),
CreateUser("PassantAli35", "passantali35@gmail.com", "01010000035"),

CreateUser("IbrahimSamy36", "ibrahimsamy36@gmail.com", "01010000036"),
CreateUser("DoaaHassan37", "doaahassan37@gmail.com", "01010000037"),
CreateUser("WaelMostafa38", "waelmostafa38@gmail.com", "01010000038"),
CreateUser("EsraaNabil39", "esraanabil39@gmail.com", "01010000039"),
CreateUser("FadyRamy40", "fadyramy40@gmail.com", "01010000040"),

CreateUser("MarinaYoussef41", "marinayoussef41@gmail.com", "01010000041"),
CreateUser("BassemKhaled42", "bassemkhaled42@gmail.com", "01010000042"),
CreateUser("RahmaTarek43", "rahmatarek43@gmail.com", "01010000043"),
CreateUser("OmarWael44", "omarwael44@gmail.com", "01010000044"),
CreateUser("JanaEssam45", "janaessam45@gmail.com", "01010000045"),

CreateUser("YassinHossam46", "yassinhossam46@gmail.com", "01010000046"),
CreateUser("MahaAdham47", "mahaadham47@gmail.com", "01010000047"),
CreateUser("KareemNader48", "kareemnader48@gmail.com", "01010000048"),
CreateUser("SalmaSherif49", "salmasherif49@gmail.com", "01010000049"),
CreateUser("ZeyadAshour50", "zeyadashour50@gmail.com", "01010000050"),
            };



            foreach (var ClinetUser in ClinetUsers)
            {
                var existingInstructor = await _userManager.FindByEmailAsync(ClinetUser.Email);

                if (existingInstructor == null)
                {
                    await _userManager.CreateAsync(ClinetUser, "User@2026");
                }
            }







        }
    }
}
