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

        public static class UserRouter
        {
            public const string prefix = Rule+"User";
            public const string Create = prefix + "/Create";

        }
    }
}
