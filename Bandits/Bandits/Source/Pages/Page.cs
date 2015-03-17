using Bandits.Usability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanditsModel;
using Bandits.Utils;

namespace Bandits
{
    public class Page : System.Web.UI.Page
    {
        private List<string> _allowableRoles = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            Authorize();
        }

        protected void AllowRole(string role)
        {
            if (!_allowableRoles.Contains(role)) _allowableRoles.Add(role);
        }

        protected void DenyRole(string role)
        {
            _allowableRoles.Remove(role);
        }

        protected void Authorize()
        {
            foreach (string role in _allowableRoles)
            {
                if (!Context.User.IsInRole(role))
                {
                    // TODO do some other logic when i have more time
                    throw new ApplicationException("You are not authorized to perform actions on this page.");
                }
            }
        }

        protected WebUser CurrentUser
        {
            get
            {
                return UserManagement.GetCurrentWebUser();
            }
        }
    }
}