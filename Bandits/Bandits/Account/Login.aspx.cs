using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BanditsModel;

namespace Bandits.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            RegisterHyperLink.NavigateUrl = "Register";
            OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];

            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }



        protected void loginControl_LoggedIn(object sender, EventArgs e)
        {
            if (HttpContext.Current == null || HttpContext.Current.Items == null || HttpContext.Current.Items["User"] == null)
            {
                    return;
                
            }

            WebUser loggedInUser = HttpContext.Current.Items["User"] as WebUser;

            if (loggedInUser != null)
            {
                HttpContext.Current.Session["CurrentUser"] = loggedInUser;
            }
        }
    }
}