using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Tradify.Data.Entities.Identity;
using Tradify.Infrastructure.Context;
using Tradify.Service.AbstractsServices.IdentityServices;

namespace Tradify.Service.Services.IdentityServices
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public UserManager<User> UserManager { get; }
        public UserService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        private bool IsEmail(string input)
        {
            return System.Net.Mail.MailAddress.TryCreate(input, out _);
        }

        private bool IsPhone(string input)
        {
            return Regex.IsMatch(input, @"^(?:\+20|0)?1[0125][0-9]{8}$");
        }

        public async Task<string> AddUser(User user, string Password)
        {
            using (var trans = applicationDbContext.Database.BeginTransaction())
            {
                try
                {
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
