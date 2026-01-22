using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Infrastructure.Repositories;

namespace Tradify.Infrastructure.Dependencies
{
    public static class InfrastructureDependencies
    {
        public static IServiceCollection AddInfrasturcureDepndencies(this IServiceCollection services) 
        {
            services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IPayoutRepository, PayoutRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductImagRepository, ProductImageRepository>();
            services.AddTransient<IProductVariantRepository, ProductVariantRepository>();
            services.AddTransient<IProductVideoRepository, ProductVideoRepository>();
            services.AddTransient<IReviewRepository, ReviewRepository>();
            services.AddTransient<IShipmentRepository, ShipmentRepository>();
            services.AddTransient<IShipmentTrackingRepository, ShipmentTrackingRepository>();
            services.AddTransient<IStoreRepository, StoreRepository>();
            services.AddTransient<ISubOrderRepository, SubOrderRepository>();
            services.AddTransient<ISellerRepository, SellerRepository>(); 

            return services;
        }
    }
}
