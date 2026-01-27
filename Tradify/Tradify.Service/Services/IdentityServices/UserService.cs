using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Tradify.Data.Entities.Identity;
using Tradify.Infrastructure.Context;
using Tradify.Service.AbstractsServices;
using Tradify.Service.AbstractsServices.IdentityServices;

namespace Tradify.Service.Services.IdentityServices
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> UserManager;
        private readonly IUrlHelper _urlHelper;
        private readonly IEmailService _emailService;

        public UserService(ApplicationDbContext applicationDbContext,IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager,IUrlHelper urlHelper,IEmailService emailService)
        {
            this.applicationDbContext = applicationDbContext;
            this.httpContextAccessor = httpContextAccessor;
            this.UserManager = userManager;
            this._urlHelper = urlHelper;
            this._emailService = emailService;
        }

        private bool IsEmail(string input)
        {
            return System.Net.Mail.MailAddress.TryCreate(input, out _);
        }

        private bool IsPhone(string input)
        {
            return Regex.IsMatch(input, @"^(?:\+20|0)?1[0125][0-9]{8}$");
        }

        public async Task<string> AddUserAsync(User user, string Password)
        {
            using (var trans = applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    var checkemail = IsEmail(user.Email);
                    var checkPhone = IsPhone(user.PhoneNumber);
                    if (!checkemail || !checkPhone) return "Add_Correct_info";
                    //if Email or Phone is Exist
                    var ExistUserEmail = await UserManager.FindByEmailAsync(user.Email);
                    var ExistUserPhonenumber = applicationDbContext.users.FirstOrDefault(x => x.PhoneNumber == user.PhoneNumber);
                    if (ExistUserEmail!=null || ExistUserPhonenumber !=null)
                    {
                        return "EmailOrPhoneIsExist";
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
                    // attach role
                    await UserManager.AddToRoleAsync(user, "User");
                    if (user.Email != null ) {
                        //Send Confirm Email
                        var code = await UserManager.GenerateEmailConfirmationTokenAsync(user);
                        var requestAccessories = httpContextAccessor.HttpContext.Request;
                        var returnURL = requestAccessories.Scheme + "://" + requestAccessories.Host + _urlHelper.Action("ConfirmEmail", "Authentication", new { userId = user.Id, code = code });

                        var message = $"To Confirm Email Click Link: <a href='{returnURL}'>Link Of Confirmation</a>";
                        await _emailService.SendEmail(user.Email, message, "Confirm Email");

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
    }
}
