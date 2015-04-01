using Bandits.Usability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanditsModel;
using Bandits.Utils;
using Bandits.Pages;

namespace Bandits
{
    public abstract class Page : System.Web.UI.Page
    {
        public abstract string PageKey { get; }
        public abstract string PageDisplay { get; }

        protected void Page_Init(object sender, EventArgs e)
        {
            Page.Title = PageDisplay;
        }

        public void AllowRole(string role) { ((MasterPage)Master).AllowRole(role); }

        public void DenyRole(string role) { ((MasterPage)Master).DenyRole(role); }

        protected WebUser CurrentUser
        {
            get
            {
                return UserManagement.GetCurrentWebUser();
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