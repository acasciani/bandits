using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bandits.CacheManagement
{
    public static partial class WebUserCache
    {
        public static IEnumerable<WebUser> GetScopedWebUser(bool reload=false)
        {
            int userId = UserManagement.GetCurrentWebUser().WebUserId;

            IEnumerable<WebUser> users = HttpContext.Current.Session["ScopedWebUsers"] as IEnumerable<WebUser>;
            if (users == null || reload)
            {
                Scoped<WebUsersControllerSoped, WebUser, Int32> c = new Scoped<WebUsersControllerSoped, WebUser, int>();
               
            }
            return null;
        }
    }
}