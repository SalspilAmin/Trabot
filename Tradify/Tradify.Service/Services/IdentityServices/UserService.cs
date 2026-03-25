using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Tradify.Data.Entities.Identity;
using Tradify.Infrastructure.Context;
using Tradify.Service.AbstractsServices;
using Tradify.Service.AbstractsServices.IdentityServices;
using Tradify.Service.AbstractsServices.WhatsappServices;
using Tradify.Service.Services.WhatsappServices;

namespace Tradify.Service.Services.IdentityServices
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> UserManager;
        private readonly IUrlHelper _urlHelper;
        private readonly IEmailService _emailService;
        private readonly IWatsappService watsappService;

        public UserService(ApplicationDbContext applicationDbContext,IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager,IUrlHelper urlHelper,IEmailService emailService,IWatsappService watsappService)
        {
            this.applicationDbContext = applicationDbContext;
            this.httpContextAccessor = httpContextAccessor;
            this.UserManager = userManager;
            this._urlHelper = urlHelper;
            this._emailService = emailService;
            this.watsappService = watsappService;
        }

        public bool IsEmail(string input)
        {
            return System.Net.Mail.MailAddress.TryCreate(input, out _);
        }

        public bool IsPhone(string input)
        {
            input = input.Replace(" ", "").Trim();
            return Regex.IsMatch(input, @"^(?:\+20|0)?1[0125][0-9]{8}$");
        }

        //public async Task SendConfirmEmail(IHttpContextAccessor httpContextAccessor,UserManager<User> userManager,User user)
        //{
        //    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
        //    var requestAccessories = httpContextAccessor.HttpContext.Request;
        //    var returnURL = requestAccessories.Scheme + "://" + requestAccessories.Host + _urlHelper.Action("ConfirmEmail", "Authentication", new { userId = user.Id, code = code });

        //    var message = $"To Confirm Email Click Link: <a href='{returnURL}'>Link Of Confirmation</a>";
        //    await _emailService.SendEmail(user.Email, message, "Confirm Email");



        //}
        public async Task<string> AddUserAsync(User user, string Password)
        {
            using (var trans = applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    var checkemail = IsEmail(user.Email);
                    var checkPhone = IsPhone(user.PhoneNumber);
                    if (!checkemail && !checkPhone)
                    {
                        return "Add_Correct_info";
                    }
                   else if(!checkemail&& checkPhone)
                    {
                        user.Email = null;
                        var ExistUserPhonenumber = applicationDbContext.users.FirstOrDefault(x => x.PhoneNumber == user.PhoneNumber);
                        if (ExistUserPhonenumber != null)
                        {
                            return "EmailOrPhoneIsExist";
                        }
                    }
                    else if(checkemail && !checkPhone)
                    
                    {
                        user.PhoneNumber=null;
                      var ExistUserEmail = await UserManager.FindByEmailAsync(user.Email);
                        if (ExistUserEmail != null) { 
                               return "EmailOrPhoneIsExist";
                        }
                    }
                  
                    
                    
                    //if username is Exist
                    var ExistUserName = await UserManager.FindByNameAsync(user.UserName);
                    if (ExistUserName != null)
                    {
                            return "UserNameIsExist";
                     
                    }
                    var createResult= await UserManager.CreateAsync(user,Password);
                    if (!createResult.Succeeded)
                    {
                        return string.Join(",",createResult.Errors.Select(x=>x.Description).ToList());
                    }
                    //  attach role


                    var AddRoleResult = await UserManager.AddToRoleAsync(user, "User");
                    if (!AddRoleResult.Succeeded)
                    {
                        return string.Join(",", AddRoleResult.Errors.Select(x => x.Description).ToList());
                    }
                    
                   
                    

                    if (user.Email != null&& checkemail==true ) {
                        //Send Confirm Email
                        var code = await UserManager.GenerateEmailConfirmationTokenAsync(user);
                        var requestAccessories = httpContextAccessor.HttpContext.Request;
                        var returnURL = requestAccessories.Scheme + "://" + requestAccessories.Host + _urlHelper.Action("ConfirmEmail", "Authentication", new { userId = user.Id, code = code });

                        var message = $"To Confirm Email Click Link: <a href='{returnURL}'>Link Of Confirmation</a>";
                        await _emailService.SendEmail(user.Email, message, "Confirm Email");

                    }
                    if (user.PhoneNumber != null && checkPhone == true)
                    {


                        string otp = new Random().Next(100000, 999999).ToString();
                        user.OTP = otp;
                        await UserManager.UpdateAsync(user);
                        var result = await watsappService.SendVerificationCodeAsync(user.PhoneNumber, otp);

                        if (result == false) return "Failed";

                    }
                    await trans.CommitAsync();
                    return "Success";
                }

                catch (Exception ex) 
        
                {

                    await trans.RollbackAsync();
                    return "Failed";

                }
            }
        }


        public async Task<User?> FindUserByEmailOrPhoneAsync(string emailOrphone)
        {

        emailOrphone = emailOrphone.Trim();

            if (emailOrphone.Contains('@'))
            {
                return await UserManager.FindByEmailAsync(emailOrphone);
            }

            return await UserManager.Users.FirstOrDefaultAsync(x=>x.PhoneNumber == emailOrphone);
        }
    }
}
