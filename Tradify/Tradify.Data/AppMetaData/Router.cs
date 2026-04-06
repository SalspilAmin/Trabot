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
            public const string Paginated = prefix + "/Paginated";
            public const string GetByID = prefix + "/Get" + SignleRoute;
            public const string UpdateProduct = prefix + "/Update" ;
            public const string Delete = prefix + "/Delete" + SignleRoute;
            public const string Category = prefix + "/Category";
            public const string MyProducts = prefix + "/MyProducts" ;
            public const string restore = prefix + "/restore" + SignleRoute;


        }
        public static class Review
        {
            public const string prefix = Rule + "Review";
            public const string Add = prefix + "/AddProduct";
            public const string Update = prefix + "/Update" ;
            public const string Delete = prefix + "/Delete" + SignleRoute;
            public const string Paginated = prefix + "/Paginated";



        }
        public static class Favorite
        {
            public const string prefix = Rule + "Favorite";
            public const string Add = prefix + "/AddProduct";
            public const string Delete = prefix + "/Delete" + SignleRoute;
            public const string Paginated = prefix + "/Paginated";
            public const string Toggle = prefix + "/toggle";

            
        }

        

        public static class ProductImage
        {
            public const string prefix = Rule + "ProductImage";
            public const string Add = prefix + "/AddProduct";
            public const string Update = prefix + "/Update" ;
            public const string Delete = prefix + "/Delete" + SignleRoute;




        }
        public static class ProductVariantImage
        {
            public const string prefix = Rule + "ProductVariantImage";
            public const string Add = prefix + "/AddProduct";
            public const string Update = prefix + "/Update";
            public const string Delete = prefix + "/Delete" + SignleRoute;




        }
        
        public static class ProductVariant
        {
            public const string prefix = Rule + "ProductVariant";

            public const string Add = prefix + "/AddProductVariant";
            public const string UpdateProductVariant = prefix + "/Update";
            public const string AddDiscount = prefix + "/AddDiscount";
            public const string DeleteDiscount = prefix + "/DeleteDiscount";
            public const string restore = prefix + "/restore" + SignleRoute;
            public const string Delete = prefix + "/Delete" + SignleRoute;
            public const string GetByProduct = prefix + "/ByProduct";
            public const string GetById = prefix + "/Get" + SignleRoute;

            


        }

        public static class Store
        {
            public const string prefix = Rule + "Store";
            public const string Add = prefix + "/AddStore";
            public const string UpdateStore = prefix + "/Update";
            public const string GetByID = prefix + "/Get" + SignleRoute;
            public const string GetMyStore = prefix + "/my-store" + SignleRoute;
            public const string ActivateStore = prefix + "/my-store/activate";
            public const string Paginated = prefix + "/Paginated";
            public const string DeactivateStore = prefix + "/my-store/Deactivate";
            public const string Delete = prefix + "/Delete" + SignleRoute;
            public const string restore = prefix + "/restore" + SignleRoute;
            public const string GetDeletedStores = prefix + "/DeletedStores";

            



        }
        public static class Fawaterak
        {
            public const string prefix = Rule + "Fawaterak";

            public const string GetPaymentMehtods = prefix + "/GetPaymentMehtods";
            public const string EInvoiceLink = prefix + "/InvoiceLink";
            public const string invoiceInitPay = prefix + "/invoiceInitPay";
            public const string WebHook = prefix + "/Webhook";
            public const string Webhookpaid_json = WebHook + "/paid_json";
            public const string WebhookCancel = WebHook + "/cancel";
            public const string Webhookfailed = WebHook + "/failed";
        }
        
        public static class Order
        {
            public const string prefix = Rule + "Order";
            public const string CreateOrder = prefix + "/Create";
            public const string UpdateOrder = prefix + "/Update";
            public const string GetOrderById = prefix + SignleRoute;
        }

    }
}
