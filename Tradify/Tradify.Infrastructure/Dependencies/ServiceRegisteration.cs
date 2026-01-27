using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Helpers;
using Tradify.Infrastructure.Context;

namespace Tradify.Infrastructure.Dependencies
{
    public static class ServiceRegisteration
    {
        public static IServiceCollection AdServiceRegisteration(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddIdentity<User, Role>(options =>
            {
                // Password Options
                options.Password.RequireDigit= true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequiredLength = 6;


                // lockout options
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 6;
                options.Lockout.AllowedForNewUsers = true;


                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;

            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            var EmailSettings = new EmailSettings();
            configuration.GetSection(nameof(EmailSettings)).Bind(EmailSettings);
            services.AddSingleton(EmailSettings);

           



            return services; 
        }
    }
}
