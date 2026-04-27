using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Infrastructure.AbstractsRepositories.UserConnectionRepositories;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Infrastructure.Repositories;
using Tradify.Infrastructure.Repositories.UserConnectionRepositories;

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
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IMessageMediaPathRepository, MessageMediaPathRepository>();
            services.AddTransient<ICommentRepository , CommentRepository>();    
            services.AddTransient<IReplyOFCommentRepository , ReplyOFCommentRepository>();  
            services.AddTransient<IImageOrVideoPathRepository , ImageOrVideoPathRepository>();  
            services.AddTransient<IInteractionWithPostRepository , InteractionWithPostRepository>();    
            services.AddTransient<IPostRepository , PostRepository>();  
            
            services.AddTransient<IRefreshTokenRepository , RefreshTokenRepository>();
            services.AddTransient<ICartRepository,CartRepository>();
            services.AddTransient<ICartProductRepository, CartProductRepository>();
           services.AddTransient<IUserConnectionRepository,UserConnectionRepository>();
            services.AddTransient<IFavoriteRepository, FavoriteRepository>();
            services.AddTransient<IProductVariantImageRepository, ProductVariantImageRepository>();
            services.AddTransient<IOrderItemsRepository, OrderItemsRepository>();
            services.AddTransient<IStoreRepository, StoreRepository>();

            services.AddTransient<IBookingsRepositorie, BookingsRepositorie>();
            services.AddTransient<ICertificationsRepositorie, CertificationsRepositorie>();
            services.AddTransient<IEducationRepositorie, EducationRepositorie>();
            services.AddTransient<IInstructorImageRepositories, InstructorImageRepositories>();
            services.AddTransient<IInstructorSchedulesRepositorie, InstructorSchedulesRepositorie>();
            services.AddTransient<IInstructorsRepositorie, InstructorsRepositorie>();
            services.AddTransient<IServiceRepositorie, ServiceRepositorie>();




            return services;
        }
    }
}
