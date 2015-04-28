using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Membership.OpenAuth;
using BanditsModel;
using Bandits.Utils;

namespace Bandits.Account
{
    public partial class Register : Page
    {
        public override string PageKey { get { return "Admin.Account.Register"; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
        }

        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            FormsAuthentication.SetAuthCookie(RegisterUser.UserName, createPersistentCookie: false);

            if (HttpContext.Current.Items != null && HttpContext.Current.Items["NewUser"] != null)
            {
                WebUser createdUser = HttpContext.Current.Items["NewUser"] as WebUser;

                if (createdUser != null)
                {
                    UserManagement.BindWebUserToSession(createdUser);
                }
            }

            string continueUrl = RegisterUser.ContinueDestinationPageUrl;
            if (!OpenAuth.IsLocalUrl(continueUrl))
            {
                continueUrl = "~/";
            }
            Response.Redirect(continueUrl);
        }
    }
}