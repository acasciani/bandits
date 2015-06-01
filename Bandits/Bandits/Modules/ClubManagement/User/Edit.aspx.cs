using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bandits.Modules.ClubManagement.User
{
    public partial class Edit : Page
    {
        public override string PageKey { get { return "EditUsers"; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the primary key off the get parameters
            int userId;
            if (Page.Request.QueryString.Get("id") == null || !int.TryParse(Page.Request.QueryString.Get("id"), out userId))
            { // unable to resolve id
                Page.Response.Redirect(ResolveUrl("~/Modules/ClubManagement/User/Default.aspx"), true);
            }
            else
            {
                if (IsPostBack) { return; }
                UpsertWizard.UpsertViewMode = Usability.UpsertViewMode.Edit;
                UpsertWizard.PrimaryKey = userId;
            }
        }
    }
}