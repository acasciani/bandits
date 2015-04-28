using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanditsModel;
using System.Web.Security;
using Bandits.Providers.Security;
using System.Web.Script.Serialization;

namespace Bandits
{
    public partial class WebUsersController : OpenAccessBaseApiController<BanditsModel.WebUser, BanditsModel.BanditsModel>
    {
        public bool HasPermission(int userId, string permission)
        {
            WebUser user = Get(userId);
            return HasPermission(user, permission);
        }

        public static bool HasPermission(WebUser user, string permission)
        {
            IEnumerable<Auth_Permission> perms = user.Permissions.Union(user.RoleAssignments.SelectMany(t=>t.Permissions));
            return perms.Where(t => t.PermissionName == permission && !t.Deny).Count() > 0;
        }
    }
}