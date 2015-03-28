using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bandits.Pages
{
    public static class SessionConstants
    {
        public static string CurrentPageKey
        {
            get { return HttpContext.Current.Session["Navigation_CurrentPageKey"].ToString(); }
            set { HttpContext.Current.Session["Navigation_CurrentPageKey"] = value.ToString(); }
        }

        public static string CurrentModuleKey
        {
            get { return HttpContext.Current.Session["Navigation_CurrentModuleKey"].ToString(); }
            set { HttpContext.Current.Session["Navigation_CurrentModuleKey"] = value.ToString(); }
        }
    }
}