using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace Bandits.CacheManagement
{
    public class AuthorizationCache
    {
        public static Dictionary<int, Auth_Role> Roles
        {
            get
            {
                Dictionary<int, Auth_Role> _roles = HttpContext.Current.Cache["UserRoles"] as Dictionary<int, Auth_Role>;
                if (_roles == null)
                {
                    using (Auth_RolesController arc = new Auth_RolesController())
                    {
                        _roles = arc.Get().ToDictionary(i => i.RoleId);
                        HttpContext.Current.Cache.Add("UserRoles", _roles, null, Cache.NoAbsoluteExpiration, new TimeSpan(4, 0, 0, 0), CacheItemPriority.Low, null);
                    }
                }
                return _roles;
            }
        }

        public static Dictionary<int, Auth_Permission> Permissions
        {
            get
            {
                Dictionary<int, Auth_Permission> _permissions = HttpContext.Current.Cache["UserPermissions"] as Dictionary<int, Auth_Permission>;
                if (_permissions == null)
                {
                    using (Auth_PermissionsController apc = new Auth_PermissionsController())
                    {
                        _permissions = apc.Get().ToDictionary(i => i.PermissionId);
                        HttpContext.Current.Cache.Add("UserPermissions", _permissions, null, Cache.NoAbsoluteExpiration, new TimeSpan(4, 0, 0, 0), CacheItemPriority.Low, null);
                    }
                }
                return _permissions;
            }
        }
    }
}