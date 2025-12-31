using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.DependencyInjection;
using Tradify.Service.ServiceBases;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;

namespace Tradify.Service.Dependencies
{
    public static  class ServicesDependencies
    {
        public static  IServiceCollection AddServicesDepencies(this IServiceCollection services)
        {
            services.AddTransient(typeof(Tradify.Service.ServiceBases.IService<>), typeof(Tradify.Service.ServiceBases.Service<>));
            services.AddTransient<ICateroriesService, CateroriesService>();
            services.AddTransient<IOrdersService, OrdersService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IPayoutService, PayoutService>();
            services.AddTransient<IProductImageService, ProductImageService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductVariantService, ProductVariantService>();
            services.AddTransient<IProductVideoService, ProductVideoService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<ISellerService, SellerService>();
            services.AddTransient<IShipmentService, ShipmentService>();
            services.AddTransient<IShipmentTrackingService, ShipmentTrackingService>();
            services.AddTransient<IStoreService, StoreService>();
            services.AddTransient<ISubOrderService, ISubOrderService>();


            return services;

        }
    }
}
