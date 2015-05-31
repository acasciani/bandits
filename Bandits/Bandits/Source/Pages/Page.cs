using Bandits.Usability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanditsModel;
using Bandits.Utils;
using Bandits.Pages;
using System.Web.Security;

namespace Bandits
{
    public abstract class Page : System.Web.UI.Page
    {
        public virtual string PageKey { get { return null; } }

        protected WebUser CurrentUser
        {
            get
            {
                WebUser user = UserManagement.GetCurrentWebUser();
                if (user == null)
                {
                    FormsAuthentication.RedirectToLoginPage();
                    return null;
                }
                else
                {
                    return UserManagement.GetCurrentWebUser();
                }
            }
        }

        protected string GetSortDirection(string column)
        {
            // By default, set the sort direction to ascending.
            string sortDirection = "ASC";

            // Retrieve the last column that was sorted.
            string sortExpression = ViewState["SortExpression"] as string;

            if (sortExpression != null)
            {
                // Check if the same column is being sorted.
                // Otherwise, the default value can be returned.
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }

            // Save new values in ViewState.
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }
    }
}