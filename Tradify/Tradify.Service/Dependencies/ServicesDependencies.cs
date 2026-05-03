using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.DependencyInjection;
using Tradify.Service.ServiceBases;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Service.AbstractsServices.IdentityServices;
using Tradify.Service.Services.IdentityServices;
using Tradify.Service.AbstractsServices.AuthenticationServices;
using Tradify.Service.Services.AuthenticationServices;
using Tradify.Service.AbstractsServices.WhatsappServices;
using Tradify.Service.Services.WhatsappServices;
using Tradify.Service.AbstractsServices.FawaterakServices;
using Tradify.Service.Services.FawaterakServices;
using Tradify.Service.AbstractsServices.AuthorizationServices;
using Tradify.Service.Services.AuthorizationServices;

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
            services.AddTransient<ISubOrderService, SubOrderService>();
            services.AddTransient<IMessageService, MessageService>();   
            services.AddTransient< IMessageMediaPathService , MessageMediaPathService>();
            services.AddTransient<ICommentService, CommentService>();   
            services.AddTransient<IReplyOFCommentService , ReplyOFCommentService>();    
            services.AddTransient<IImageOrVideoPathService , ImageOrVideoPathService>();    
            services.AddTransient<IInteractionWithPostService , InteractionWithPostService>();  
            services.AddTransient<IPostService , PostService>();    
            services.AddTransient<IFileService , FileService>();
            services.AddTransient<IEmailService , EmailService>();
            services.AddTransient<IUserService,UserService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IWatsappService,WatsappService>();
            services.AddScoped<IFawaterakServices, FawaterakServices>();
            services.AddTransient<ICartService,CartService>();
            services.AddTransient<ICartProductService, CartProductService>();
            services.AddTransient<IAuthorizationService, AuthorizationService>();
            services.AddTransient<IFavoriteService, FavoriteService>();
            services.AddTransient<IProductVariantImageService, ProductVariantImageService>();
            services.AddTransient<IOrderItemsService, OrderItemsService>();
            services.AddTransient<IStoreImageService, StoreImageService>();
            services.AddTransient<IBookingsService, BookingsService>();
            services.AddTransient<ICertificationsService, CertificationsService>();
            services.AddTransient<IEducationService, EducationService>();
            services.AddTransient<IInstructorImageService, InstructorImageService>();
            services.AddTransient<IInstructorSchedulesService, InstructorSchedulesService>();
            services.AddTransient<IInstructorsService, InstructorsService>();
            services.AddTransient<IServiceService, ServiceService>();



            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();


            



            return services;

        }
    }
}
