using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Bandits
{
    public abstract class LoggedInPage : Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (CurrentUser == null)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
        }
    }
}