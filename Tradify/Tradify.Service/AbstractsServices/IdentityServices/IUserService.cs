using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Identity;

namespace Tradify.Service.AbstractsServices.IdentityServices
{
    public interface IUserService
    {
       

        public Task<string>  AddUserAsync(User user,string Password);
        public bool IsPhone(string input);
        public bool IsEmail(string input);
        public  Task<User?> FindUserByEmailOrPhoneAsync(string emailOrphone);
    }
}
