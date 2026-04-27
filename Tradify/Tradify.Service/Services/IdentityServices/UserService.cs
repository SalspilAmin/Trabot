using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Helpers;
using Tradify.Data.Helpers.Results;
using Tradify.Infrastructure.Context;
using Tradify.Service.AbstractsServices;
using Tradify.Service.AbstractsServices.AuthenticationServices;
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
        private readonly JwtSettings jwtSettings;
        private readonly ICartService _cartService; 

        public UserService(ApplicationDbContext applicationDbContext,IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager,IUrlHelper urlHelper,IEmailService emailService,IWatsappService watsappService
            , JwtSettings jwtSettings, ICartService _cartService)
        {
            this.applicationDbContext = applicationDbContext;
            this.httpContextAccessor = httpContextAccessor;
            this.UserManager = userManager;
            this._urlHelper = urlHelper;
            this._emailService = emailService;
            this.watsappService = watsappService;
            this.jwtSettings = jwtSettings;
            this._cartService=_cartService;
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
        public async Task<(string,int ?)> AddUserAsync(User user, string Password)
        {
            using (var trans = applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    var checkemail = IsEmail(user.Email);
                    var checkPhone = IsPhone(user.PhoneNumber);
                    if (!checkemail && !checkPhone)
                    {
                        return ("Add_Correct_info",null);
                    }
                   else if(!checkemail&& checkPhone)
                    {
                        user.Email = null;
                        var ExistUserPhonenumber = applicationDbContext.users.FirstOrDefault(x => x.PhoneNumber == user.PhoneNumber);
                        if (ExistUserPhonenumber != null)
                        {
                            return ("EmailOrPhoneIsExist", null);
                        }
                    }
                    else if(checkemail && !checkPhone)
                    
                    {
                        user.PhoneNumber=null;
                      var ExistUserEmail = await UserManager.FindByEmailAsync(user.Email);
                        if (ExistUserEmail != null) { 
                               return ("EmailOrPhoneIsExist", null);
                        }
                    }
                  
                    
                    
                    //if username is Exist
                    var ExistUserName = await UserManager.FindByNameAsync(user.UserName);
                    if (ExistUserName != null)
                    {
                            return ("UserNameIsExist", null);
                     
                    }
                    var createResult= await UserManager.CreateAsync(user,Password);
                    if (!createResult.Succeeded)
                    {
                        return (string.Join(",",createResult.Errors.Select(x=>x.Description).ToList()),null);
                    }
                    //  attach role


                    var AddRoleResult = await UserManager.AddToRoleAsync(user, "User");
                    if (!AddRoleResult.Succeeded)
                    {
                        return (string.Join(",", AddRoleResult.Errors.Select(x => x.Description).ToList()), null);
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

                        if (result == false) return ("Failed", null);

                    }
                    var CreateCartResult = await _cartService.AddAsync(new Data.Entities.Cart() { User = user ,UserId=user.Id});
                    
                    if(CreateCartResult==null) return ("Failed", null);
                    user.CartId = CreateCartResult.Id;
                    await trans.CommitAsync();
                    return ("Success",user.Id);
                }

                catch (Exception ex)  
        
                {

                    await trans.RollbackAsync();
                    return ("Failed", null);

                }
            }
        }


        public async Task<User?> FindUserByEmailOrPhoneAsync(string emailOrphone)
        {

        emailOrphone = emailOrphone.Trim();
            var users= await UserManager.Users.Where(x=>x.IsDeleted == false).ToListAsync();

            if (emailOrphone.Contains('@'))
            {
                return users.FirstOrDefault(x=>x.Email==emailOrphone);
            }

            return  users.FirstOrDefault(x=>x.PhoneNumber == emailOrphone);
        }

        public async Task<UserInfoFromToken?> GetUserInformationByToken(string token)
        {
            var result = await ValidateToken(token);
            if (result != "NotExpired") return null;
            var Token = ReadJWTToken(token);

            var claims = Token.Claims;

            var userInfo = new UserInfoFromToken
            {
                UserId = claims
              .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,

                Email = claims
              .FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,

                UserName = claims
              .FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value,

                Roles = claims
              .Where(x => x.Type == ClaimTypes.Role)
              .Select(x => x.Value)
              .ToList()
            };

            return userInfo;

        }




        private async Task<string> ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameter = new TokenValidationParameters()
            {
                ValidateIssuer = jwtSettings.ValidateIssuer,
                ValidateAudience = jwtSettings.ValidateAudience,
                ValidateLifetime = jwtSettings.ValidateLifeTime,
                ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret))



            };
            try
            {
                var validation = handler.ValidateToken(accessToken, parameter, out SecurityToken securityToken);

                if (validation == null)
                {
                    return "InvalidToken";
                }
                return "NotExpired";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private JwtSecurityToken ReadJWTToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));

            }
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);

            return response;
        }

    }
}
