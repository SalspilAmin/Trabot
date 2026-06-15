using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Identity;

namespace Tradify.Data.AppMetaData
{
    public static class Router
    {
        public const string root = "Api";
        public const string version = "V1";
        public const string Rule = root + "/" + version + "/";
        public const string SignleRoute = "/{id}";

        public static class UserRouter
        {
            public const string prefix = Rule+"User";
            public const string Create = prefix + "/Create";
            public const string ChangePassword = prefix + "/ChangePassword";
            public const string Paginated = prefix + "/Paginated";
            public const string GetByID = prefix +"/Get"+SignleRoute;
            public const string Delete = prefix + "/Delete" + SignleRoute;
            public const string GetUserByToken = prefix + "/GetUserByToken";

        }
        public static class Authentication
        {
            public const string prefix = Rule + "Authentication";
            public const string LogIN = prefix + "/LogIn";
            public const string RefreshToken = prefix + "/RefreshToken";
            public const string ConfirmEmail =prefix+"/ConfirmEmail";
            public const string ConfrimPhone = prefix + "/ConfrimPhone";
            public const string SendResetPassword = prefix + "/SendResetPasswordCode";
            public const string ConfirmResetPassword = prefix + "/ConfrimResetPassword";
            public const string ResetPassword = prefix + "/ResetPassword";
            public const string LoginGoogle = prefix + "/LogInGoogle";
            public const string LoginGoogleCallBack = prefix + "/GoogleCallBack";
            public const string GoogleResult = prefix + "/GoogleResult";
        }
        public static class Authorization
        {
            public const string prefix = Rule + "Authorization";
            public const string Role = prefix + "/Role";
            public const string GitList = Role + "/GitList";
            public const string GitByID = Role + "Get" + SignleRoute;
            public const string ManageUserRolesList = Role + "/ManageUserRolesList"+ SignleRoute;
            public const string Create = Role + "/create";
            public const string Edit = Role + "/Edit";
            public const string UpdateUserRoles = Role + "/UpdateUserRoles";
            public const string Delete = Role + "/Delete"+SignleRoute;
            public const string Claim = prefix + "/Claims";
            public const string ManageUserClaims = Claim + "/ManageUserClaims";
            public const string UpdateUserClaims = Claim + "/UpdateUserClaims";

        }
        public static class Product
        {
            public const string prefix = Rule + "Product";
            public const string Add = prefix + "/AddProduct";
            public const string AddWithImage = prefix + "/AddWithImage";
            public const string List = prefix + "/List";
            
            public const string Discount = prefix + "/Discount";

            public const string Search = prefix + "/Search";
            public const string Store = prefix + "/Store";

            public const string AddDiscount = prefix + "/AddDiscount";
            public const string DeleteDiscount = prefix + "/DeleteDiscount";
            public const string Paginated = prefix + "/Paginated";
            public const string BestSelling = prefix + "/BestSelling";


            
            public const string GetByID = prefix + "/Get" + SignleRoute;
            public const string UpdateProduct = prefix + "/Update" ;
            public const string Delete = prefix + "/Delete" + SignleRoute;
            public const string Category = prefix + "/Category";
            public const string MyProducts = prefix + "/MyProducts" ;
            public const string restore = prefix + "/restore" + SignleRoute;


        }
        public static class Dashpoard 
        {
            public const string prefix = Rule + "Dashpoard";
            public const string Admin = prefix + "/admin" ;
            public const string OrdersChart = prefix + "/admin"+ "/orders-chart";
        public const string BookingChart = prefix + "/admin" + "/booking-chart";

        public const string RevenueChart = prefix + "/admin" + "/RevenueChart";
            public const string TopStores = prefix + "/admin" + "/top-stores";

            public const string SellerProduct = prefix + "/sellerProduct";


            public const string SellerProductOrdersChart = prefix + "/sellerProduct" + "/orders-chart";
            public const string SellerProductRevenueChart = prefix + "/sellerProduct" + "/RevenueChart";
            public const string TopProductsSelling = prefix + "/sellerProduct" + "/top-products-Selling";
            public const string TopRatedProducts = prefix + "/sellerProduct" + "/top-Rated-Products";


            public const string SellerService = prefix + "/sellerService";

            public const string SellerServiceBookingChart = prefix + "/sellerService" + "/BookingChart";
            public const string TopInstructor = prefix + "/sellerService" + "/top-Instructor";

            public const string Instructor = prefix + "/instructor";

            public const string InstructorSessionsChart = prefix + "/instructor" + "/SessionsChart";
            public const string UpcomingAppointments = prefix + "/instructor" + "/upcoming-appointments";

            
        }

        public static class Instructor
            {
                   public const string prefix = Rule + "Instructor";
                   public const string Add = prefix + "/AddInstructor";
                   public const string AddWithImage = prefix + "/AddWithImage";
                   public const string JopTitle = prefix + "/JopTitle";
                   public const string GetByID = prefix + "/Get" + SignleRoute;
                   public const string Paginated = prefix + "/Paginated";
                   public const string BySeller = prefix + "/BySeller";
                   public const string AddDiscount = prefix + "/AddDiscount";
                   public const string DeleteDiscount = prefix + "/DeleteDiscount";
                   public const string WithDiscount = prefix + "/WithDiscount";
                   public const string Update = prefix + "/Update";
                   public const string Active = prefix + "/Active" + SignleRoute;
                   public const string DisActive = prefix + "/DisActive" + SignleRoute;


        }

        public static class InstructorEducation
        {
            public const string prefix = Rule + "InstructorEducation";
            public const string Add = prefix + "/AddEducation";
            public const string GetByInstructor = prefix + "/ByInstructor";
            public const string Delete = prefix + "/Delete" + SignleRoute;

            public const string UpdateEducation = prefix + "/Update";

        }
        public static class Booking
        {
            public const string prefix = Rule + "Booking";
            public const string Add = prefix + "/AddBooking";
            public const string GetUserBookings = prefix + "/GetUserBookings";
            public const string GetInstructorBooking = prefix + "/GetInstructorBooking";
            public const string RescheduleBooking = prefix + "/Reschedule";

            public const string CanceldBooking = prefix + "/Canceld";
            

        }
        
        public static class InstructorService
        {
            public const string prefix = Rule + "InstructorService";
            public const string Add = prefix + "/AddService";
            public const string GetByInstructor = prefix + "/ByInstructor";

            public const string Delete = prefix + "/Delete" + SignleRoute;

            public const string UpdateService = prefix + "/Update";
        }

        public static class InstructorSchedules
        {
            public const string prefix = Rule + "InstructorSchedules";
            public const string Add = prefix + "/AddSchedules";
            public const string GetByInstructor = prefix + "/ByInstructor";
            public const string GetNotAvilable = prefix + "/NotAvilable";

            public const string Delete = prefix + "/Delete" + SignleRoute;
            public const string Restore = prefix + "/Restore" + SignleRoute;

            public const string UpdateSchedules = prefix + "/Update";
        }
        
        public static class InstructorCertification
        {
            public const string prefix = Rule + "InstructorCertification";
            public const string Add = prefix + "/AddCertification";
            public const string GetByInstructor = prefix + "/ByInstructor";

            public const string Delete = prefix + "/Delete" + SignleRoute;

            public const string UpdateCertification = prefix + "/Update";

        }
        
        public static class ShipmentTracking
        {
            public const string prefix = Rule + "ShipmentTracking";
            public const string Add = prefix + "/AddShipmentTracking/{subOrderId}";


            public const string UpdateStatus = prefix + "/UpdateShipmentStatus";
            public const string GetShipmentBySubOrder = prefix + "/GetTracking/{subOrderId}";
          
            public const string BySeller = Rule + "Shipment" + "/BySeller";

            public const string BySubShipment = prefix + "/GetTracking/{shipmentId}";

            



        }
        public static class Review
        {
            public const string prefix = Rule + "Review";
            public const string AddProductReview = prefix + "/AddProductReview";
            public const string AddInstructorReview = prefix + "/AddInstructorReview";

            public const string Update = prefix + "/Update" ;
            public const string Delete = prefix + "/Delete" + SignleRoute;
            public const string ProductReviews = prefix + "/ProductReviews";
            public const string InstructorReviews = prefix + "/InstructorReviews";



        }
        public static class Category
        {
            public const string prefix = Rule + "Category";
            public const string Add = prefix + "/addCategory";
            public const string Update = prefix + "/update";
            public const string Delete = prefix + "/delete" + SignleRoute;
            public const string GetAll = prefix + "/getAll";
            public const string Tree = prefix + "/tree";
            public const string GetByID = prefix + "/Get" + SignleRoute;
            public const string restore = prefix + "/restore" + SignleRoute;





        }

        public static class Favorite
        {
            public const string prefix = Rule + "Favorite";
            public const string Add = prefix + "/AddFavorite";
            public const string Delete = prefix + "/Delete" + SignleRoute;
            public const string Paginated = prefix + "/Paginated";
            public const string Toggle = prefix + "/toggle";

            
        }


        
        public static class InstructorImage
        {
            public const string prefix = Rule + "InstructorImage";
            public const string Add = prefix + "/addInstructorImage";
            public const string Update = prefix + "/update";
            public const string Delete = prefix + "/delete" + SignleRoute;




        }

        public static class StoreImage
        {
            public const string prefix = Rule + "StoreImage";
            public const string Add = prefix + "/addStoreImage";
            public const string Update = prefix + "/update";
            public const string Delete = prefix + "/delete" + SignleRoute;




        }
        public static class ProductImage
        {
            public const string prefix = Rule + "ProductImage";
            public const string Add = prefix + "/addProductImage";
            public const string Update = prefix + "/update" ;
            public const string Delete = prefix + "/delete" + SignleRoute;




        }
        public static class ProductVariantImage
        {
            public const string prefix = Rule + "ProductVariantImage";
            public const string Add = prefix + "/AddProductVariantImage";
            public const string Update = prefix + "/update";
            public const string Delete = prefix + "/delete" + SignleRoute;




        }
        
        public static class ProductVariant
        {
            public const string prefix = Rule + "ProductVariant";

            public const string Add = prefix + "/AddProductVariant";
            public const string AddWithImage = prefix + "/AddProductVariantWithImage";

            
            public const string UpdateProductVariant = prefix + "/Update";
            public const string AddDiscount = prefix + "/AddDiscount";
            public const string DeleteDiscount = prefix + "/DeleteDiscount";
            public const string restore = prefix + "/restore" + SignleRoute;
            public const string Delete = prefix + "/Delete" + SignleRoute;
            public const string GetByProduct = prefix + "/ByProduct";
            public const string GetById = prefix + "/Get" + SignleRoute;
            public const string List = prefix + "/List";



        }

        public static class Store
        {
            public const string prefix = Rule + "Store";
            public const string Add = prefix + "/AddStore";
            public const string AddWithImage = prefix + "/AddStoreWithImage";

            public const string UpdateStore = prefix + "/Update";
            public const string GetByID = prefix + "/Get" + SignleRoute;
            public const string GetMyStore = prefix + "/my-store" + SignleRoute;
            public const string ActivateStore = prefix + "/my-store/activate";
            public const string Paginated = prefix + "/Paginated";
            public const string DeactivateStore = prefix + "/my-store/Deactivate";
            public const string Delete = prefix + "/Delete" + SignleRoute;
            public const string restore = prefix + "/restore" + SignleRoute;
            public const string GetDeletedStores = prefix + "/DeletedStores";
            public const string List = prefix + "/List";


            public const string AddStoreServiceDiscount = prefix + "/AddStoreServiceDiscount";
            public const string DeleteStoreServiceDiscount = prefix + "/DeleteStoreServiceDiscount";



        }
        public static class Fawaterak
        {
            public const string prefix = Rule + "Fawaterak";

            public const string GetPaymentMehtods = prefix + "/GetPaymentMehtods";
            public const string EInvoiceLink = prefix + "/InvoiceLink";
            public const string invoiceInitPay = prefix + "/invoiceInitPay";
            public const string WebHook = prefix + "/Webhook";
            public const string WebHookEnvoiceLInk = prefix + "/WebHookEnvoiceLInk";
            public const string Webhookpaid_json = WebHook + "/paid_json";
            public const string WebhookCancel = WebHook + "/cancel";
            public const string Webhookfailed = WebHook + "/failed";
        }
        
        public static class Order
        {
            public const string prefix = Rule + "Order";
            public const string CreateOrder = prefix + "/Create";
            public const string UpdateOrder = prefix + "/Update";
            public const string Paginated = prefix + "/Paginated";
            public const string Delete = prefix + "/Delete" + SignleRoute;

            public const string GetOrderById = prefix + SignleRoute;
        }

        public static class SupOrder
        {
            public const string prefix = Rule + "SupOrder";
           
            public const string Paginated = prefix + "/Paginated";
            public const string BySeller = prefix + "/BySeller";

            
            public const string GetByOrder = prefix + "/ByOrder";

        }



        public static class Seller
        {
            public const string prefix = Rule + "Seller";
            public const string Create = prefix + "/Create";
            public const string Paginated = prefix + "/Paginated";
            public const string GetByID = prefix + "/Get" + SignleRoute;
            public const string Profile = prefix + "/Profile" + SignleRoute;
            public const string Update = prefix + "/Update";
            public const string Active = prefix + "/Active" + SignleRoute;
            public const string DisActive = prefix + "/DisActive" + SignleRoute;
            public const string NotHaveStore = prefix + "/notHaveStore";

            
        }

        public static class Cart
        {
            public const string prefix = Rule + "Cart";
            public const string GetByToken = prefix + "/GetByToken";
            public const string UpdateCart = prefix + "Update";
            public const string AddToCart = prefix + "/AddToCart";
        }
        public static class Post
        {

            public const string prefix = Rule + "Post";
            public const string AddPost = prefix + "/AddPost";
            public const string GetPostsOfUserByID = prefix+ "/GetPostsOfUserByID"+ SignleRoute;
            public const string GetPostByID = prefix + "/GetPostByID" + SignleRoute;
            public const string GetPosts = prefix + "/GetPosts";


        }

        public static class Comments {
            public const string prefix = Rule + "Comments";
            
            public const string Addcomment = prefix + "/Add";
            public const string Updatecomment = prefix + "/update";
            public const string Deletecomment = prefix + "/Delete";
            public const string GetCommentsByPostId = prefix + "/Get/ByPostId";
            public const string GetCommentsByCommentId = prefix + "/Get/ByCommentId";
          
        }
        public static class ReplayOnComments
        {
            public const string prefix = Rule + "ReplayOnComments";
            public const string AddReplayComment = prefix + "/Add";
            public const string UpdateReplayComment = prefix + "/update";
            public const string DeleteReplayComment = prefix + "/Delete";
            public const string GetReplayesByCommentId = prefix + "/Get/ByCommentId";
            public const string Get = prefix + "/Get/ByReplayId";
        }
        public static class Interaction
        {

            public const string prefix = Rule + "Interaction";
            public const string AddInteraction = prefix + "/Add";
            public const string UpdateInteraction = prefix + "/Update";
            public const string DeleteInteraction = prefix + "/Delete" + SignleRoute;
            public const string GetInteractionByPostId = prefix + "/GetInteractions/post" + SignleRoute;
            public const string GetInteractionById = prefix + "/GetInteractions" + SignleRoute;
            public const string GetUserInteractionOnPost = prefix + "GetUserInteractionPost";
        }

       

       

       




    }
}
