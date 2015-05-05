using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanditsModel;
using System.Web.Security;
using Bandits.Providers.Security;
using System.Web.Script.Serialization;
using Telerik.OpenAccess;

namespace Bandits
{
    public partial class WebUsersController : OpenAccessBaseApiController<BanditsModel.WebUser, BanditsModel.BanditsModel>
    {
        public bool HasPermission(int userId, string permission)
        {
            WebUser user = Get(userId);
            return HasPermission(user, permission);
        }

        /// <summary>
        /// This should ONLY be used to see if the user is able to do a certain permission. Some scopes may not allow this permission, so allowing them to do this 
        /// permission for every scope is invalid and outright wrong.
        /// </summary>
        public static bool HasPermission(WebUser user, string permission)
        {
            permission = permission.ToLower();

            // Permission can be on an individual Auth Assignment or on an Auth Role
            using (BanditsModel.BanditsModel model = new BanditsModel.BanditsModel())
            {
                // Get all the assignments (group + individual) for the specified user
                IQueryable<Auth_Assignment> assignments = model.WebUsers.Where(i => i.WebUserId == user.WebUserId).Include(i => i.Auth_Assignments).SelectMany(i => i.Auth_Assignments).Include(i => i.Auth_ScopeAssignments).Include(i=>i.Permission).Include(i=>i.Role);

                // If he has assignments, continue with logic other he doesn't not have permission and we can return false.
                if (assignments.Count() > 0)
                {
                    // Get all the individual assignments (marked by IsExplicit = true) and get all the role assignments
                    var individuals = assignments.Where(i => i.IsExplicit && i.Permission.PermissionName.ToLower() == permission).SelectMany(i => i.Auth_ScopeAssignments);
                    var groups = assignments.Where(i => !i.IsExplicit);

                    // Get all the individual assignments that the scope is allowed, then get those that are denied. Then get all the permissions
                    var individualsAllowed = individuals.Where(i=>!i.Deny);
                    var individualsDenied = individuals.Where(i => i.Deny);

                    // Get all the group assignments that the scope is allowed.
                    var groupPermissions = groups.Select(i => i.Role).Include(i => i.Permissions).SelectMany(i => i.Permissions).Select(i => i.PermissionName.ToLower());
                    var groupsPermissioned = groups.Where(i => groupPermissions.Contains(permission)).SelectMany(i=>i.Auth_ScopeAssignments);
                    var groupsAllowed = groupsPermissioned.Where(i => !i.Deny);
                    var groupsDenied = groupsPermissioned.Where(i => i.Deny);

                    //IQueryable<Auth_Permission> alloweds = individualsAllowed.Union(groupsAllowed);
                    //IQueryable<Auth_Permission> denieds = individualsDenied.Union(groupsDenied);

                    //IQueryable<Auth_Permission> allowed = alloweds.Where(i => i.PermissionName.ToLower() == permission).Except(denieds.Where(i => i.PermissionName.ToLower() == permission));
                    //return allowed.Count() > 0;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}