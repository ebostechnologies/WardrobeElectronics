using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eBosCRM.Models;

namespace eBosCRM.Common
{
    public static class PermissionManager
    {
        public static bool UserHasPermission(PermissionGroupTypes permission, params PermissionGroup[] permissionGroups)
        {
            return permissionGroups.Any(pg => pg.PermissionGroupName.ToLower().Equals(permission.ToString().ToLower()));
        }
    }
}