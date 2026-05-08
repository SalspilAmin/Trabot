using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.RealTimeService.HubServices;

namespace Tradify.RealTimeService.AddDependencies
{
    public static class RealTimeDependencies
    {
        public static IServiceCollection AddRealTimeDepndencies(this IServiceCollection services)
        {

            services.AddScoped<PostHubService>();
            services.AddScoped<MessageHubService>();
          


            return services;
        }
    }
}
