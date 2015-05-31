using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bandits.SessionManagement;

namespace Bandits.Modules.ClubManagement
{
    public partial class Default : Page
    {
        public override string PageKey { get { return "ViewUsers"; } }

        public class UserResult
        {
            public string Email { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.RequirePermission(Permissions.ViewUser);

            if (IsPostBack) { return; }

            BindUsers(reload:true);
        }

        private void BindUsers(bool reload=false)
        {
            List<UserResult> results = SearchResults<UserResult>.GetSession();

            if (results == null || reload)
            {
                Scoped<WebUsersControllerSoped, WebUser, Int32> controller = new Scoped<WebUsersControllerSoped, WebUser, Int32>();
                results = controller.GetScopedObjects(CurrentUser.WebUserId, Permissions.ViewUser).Select(i => new UserResult()
                {
                    Email = i.Email
                }).ToList();

                SearchResults<UserResult>.SetSession(results);
            }

            Results.DataSource = results;
            Results.DataBind();
        }
    }
}