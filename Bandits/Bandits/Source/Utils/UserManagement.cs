using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanditsModel;
using System.Web.Security;

namespace Bandits.Utils
{
    public static class UserManagement
    {
        public static WebUser GetCurrentWebUser()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated && HttpContext.Current.Session != null)
            {
                WebUser user = HttpContext.Current.Session["CurrentUser"] as WebUser;

                if (HttpContext.Current.User.Identity.Name == user.Email)
                {
                    return user;
                }
            }

            return null;
        }
    }
}