using System;
using System.Collections.Generic;
using System.Text;

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
        }
        public static class Product
        {
            public const string prefix = Rule + "Product";
            public const string Add = prefix + "/AddProduct";
        }
        public static class Fawaterak
        {
            public const string prefix = Rule + "Fawaterak";

            public const string GetPaymentMehtods = prefix + "/GetPaymentMehtods";
            public const string EInvoiceLink = prefix + "/InvoiceLink";
        }

    }
}
