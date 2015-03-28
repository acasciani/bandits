using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BanditsModel;
using Bandits.Utils;

namespace Bandits.Account
{
    public partial class Login : Page
    {
        public const string PageKeyConst = "Admin.Account.Login";
        public const string PageDisplayConst = "Dashboard Login";
        public const string PageLinkConst = "/Account/Login.aspx";
        public override string PageKey { get { return PageKeyConst; } }
        public override string PageDisplay { get { return PageDisplayConst; } }


        protected void Page_Load(object sender, EventArgs e)
        {
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
            if (HttpContext.Current.Items != null && HttpContext.Current.Items["User"] != null)
            {
                WebUser loggedInUser = HttpContext.Current.Items["User"] as WebUser;

                if (loggedInUser != null)
                {
                    UserManagement.BindWebUserToSession(loggedInUser);
                }                
            }
        }
    }
}