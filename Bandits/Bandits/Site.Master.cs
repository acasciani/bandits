using Bandits.Usability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bandits.Utils;

namespace Bandits
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadLeftNav();
            LoadLocalPageTitle();
        }

        private void LoadLocalPageTitle()
        {
            MasterPage mp = Page.Master as MasterPage;
            if (mp != null)
            {
                PageTitleLocal.Visible = mp.IsLocalTitleVisible;
            }
        }

        private void LoadLeftNav()
        {
            System.Web.UI.MasterPage mpGeneric = Page.Master;

            while (mpGeneric != null)
            {
                MasterPage mp = mpGeneric as MasterPage;
                if (mp != null && mp.LeftNavContents != null && mp.LeftNavContents.Count() > 0)
                { // The next level up, the left nav has contents.
                    LeftNavigation.DataSource = mp.LeftNavContents;
                    LeftNavigation.DataBind();
                    return;
                }

                mpGeneric = mpGeneric.Master;
            }
        }

        public override IList<LeftNavItem> LeftNavContents { get { return Modules.LeftNav.Items; } }
        public override bool IsLocalTitleVisible { get { return false; } }
    }
}