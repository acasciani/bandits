using Bandits.Pages;
using Bandits.Usability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bandits
{
    public abstract class MasterPage : System.Web.UI.MasterPage
    {
        private List<string> _allowableRoles = new List<string>();

        public string ActiveLinkKey { get { return ((Page)Page).PageKey; } }

        public abstract string ModuleCacheKey { get; }

        public void AllowRole(string role) { if (!_allowableRoles.Contains(role)) _allowableRoles.Add(role); }

        public void DenyRole(string role) { _allowableRoles.Remove(role); }

        private void Authorize()
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

        protected void Page_Load(object sender, EventArgs e)
        {
            Authorize();
        }
    }
}