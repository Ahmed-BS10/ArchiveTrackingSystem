﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveTrackingSystem.Core.Routes
{
    public static class Route
    {
        public const string root = "Api";
        public const string version = "V1";
        public const string Rule = $"{root}/{version}/";

        public static class UserRouting
        {
            public const string Prefix = $"{Rule}User/";
            public const string List = $"{Prefix}List";
            public const string GetById = $"{Prefix}{"Id"}";
            public const string Create = $"{Prefix}Create";
            public const string Edit = $"{Prefix}Edit";
            public const string Delete = $"{Prefix}Delete{"Id"}";

        }
        public static class RoleRouting
        {
            public const string Prefix = $"{Rule}Role/";
            public const string List = $"{Prefix}List";
            public const string Create = $"{Prefix}Create";
            public const string Edit = $"{Prefix}Edit";
            public const string Delete = $"{Prefix}Delete{"Id"}";

        }
        public static class AuthorizationRouting
        {
            public const string Prefix = $"{Rule}Authorization/";
            public const string Add = $"{Prefix}AddUserRole";
            public const string Edit = $"{Prefix}Edit";
            public const string Delete = $"{Prefix}DeleteUserRole{"Id"}";

        }
    }
}
