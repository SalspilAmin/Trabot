using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Tradify.Core.Behaviors;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Dependencies
{
    public static class CoreDependencies
    {

        public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
        {
            services.AddScoped<LocalizationService>();

            //  Validators  
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidateBehavior<,>));
            


            return services;
        }
    }
}
