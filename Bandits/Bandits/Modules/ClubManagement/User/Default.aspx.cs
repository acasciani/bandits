using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bandits.Modules.ClubManagement
{
    public partial class Default : Page
    {
        public class SearchResult
        {
            public string Email { get; set; }
        }

        public override string PageKey { get { return "ClubManagement.User.ViewAll"; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            WebUsersController.HasPermission(CurrentUser, Permissions.ViewUser);

            BindUsers();
            Scoped<ProgramsControllerScoped, Program, Int32> controller = new Scoped<ProgramsControllerScoped, Program, Int32>();

            var test1 = controller.GetScopedObjects(3, Bandits.Modules.ProgramManagement.Permissions.ViewAllPrograms); // alex
            var test2 = controller.GetScopedObjects(7, Bandits.Modules.ProgramManagement.Permissions.ViewAllPrograms); // jerry
            var test3 = controller.GetScopedObjects(10, Bandits.Modules.ProgramManagement.Permissions.ViewAllPrograms); // syd
            var test4 = controller.GetScopedObjects(13, Bandits.Modules.ProgramManagement.Permissions.ViewAllPrograms); // dave k
            var test5 = controller.GetScopedObjects(14, Bandits.Modules.ProgramManagement.Permissions.ViewAllPrograms); // cheryl c
            var test6 = controller.GetScopedObjects(16, Bandits.Modules.ProgramManagement.Permissions.ViewAllPrograms); // betsy
            var test7 = controller.GetScopedObjects(17, Bandits.Modules.ProgramManagement.Permissions.ViewAllPrograms); // todd
        }



        private void BindUsers(bool reload=false)
        {
            IList<SearchResult> results = Session["Results"] as IList<SearchResult>;
            if (results == null || reload)
            {
                Scoped<WebUsersControllerSoped, WebUser, Int32> controller = new Scoped<WebUsersControllerSoped, WebUser, Int32>();
                results = controller.GetScopedObjects(CurrentUser.WebUserId, Permissions.ViewUser).Select(i => new SearchResult()
                {
                    Email = i.Email
                }).ToList();
                Session["Results"] = results;
            }

            Results.DataSource = results;
            Results.DataBind();
        }
    }
}