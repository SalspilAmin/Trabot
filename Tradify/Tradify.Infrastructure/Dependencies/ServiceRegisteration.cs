using Azure.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Helpers;
using Tradify.Data.Helpers.Fawaterak;
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
                options.User.RequireUniqueEmail = false;
                options.SignIn.RequireConfirmedEmail = true;

            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            var EmailSettings = new EmailSettings();
            configuration.GetSection(nameof(EmailSettings)).Bind(EmailSettings);
            var jwtSettings = new JwtSettings();
            configuration.GetSection(nameof(JwtSettings)).Bind(jwtSettings);
            services.AddSingleton(EmailSettings);
            services.AddSingleton(jwtSettings);
            var TwilioSettings = new TwilioSettings();
            configuration.GetSection(nameof(TwilioSettings)).Bind(TwilioSettings);
            services.AddSingleton(TwilioSettings);
            var FawaterakOptions = new Tradify.Data.Helpers.Fawaterak.FawaterakOptions();
            configuration.GetSection(nameof(FawaterakOptions)).Bind(FawaterakOptions);
            services.AddSingleton(FawaterakOptions);
            services.AddHttpClient();
            var OAuthSettings = new OAuthSettings();    
            configuration.GetSection(nameof(OAuthSettings)).Bind(OAuthSettings);
            services.AddSingleton(OAuthSettings);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
          .AddJwtBearer(x =>
          {
              x.RequireHttpsMetadata = false;
              x.SaveToken = true;
              x.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = jwtSettings.ValidateIssuer,
                  ValidIssuers = new[] { jwtSettings.Issuer },
                  ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                  ValidAudience = jwtSettings.Audience,
                  ValidateAudience = jwtSettings.ValidateAudience,
                  ValidateLifetime = jwtSettings.ValidateLifeTime,
              };
              x.Events = new JwtBearerEvents
              {
                  OnMessageReceived = context =>
                  {
                      var accesstoken = context.Request.Query["access_token"];
                      if (!string.IsNullOrEmpty(accesstoken))
                      {
                          context.Token = accesstoken;
                      }

                      return Task.CompletedTask;
                  }
              };
          });
            services.AddAuthorization(option =>
            {
                option.AddPolicy("CreateProduct", policy =>
                {
                    policy.RequireClaim("Create Product", "True");
                });
                option.AddPolicy("DeleteProduct", policy =>
                {
                    policy.RequireClaim("Delete Product", "True");
                });
                option.AddPolicy("EditProduct", policy =>
                {
                    policy.RequireClaim("Edit Product", "True");
                });
            });
            services.AddSignalR();



            return services; 
        }
    }
}
