using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bandits.Modules.ProgramManagement
{
    public partial class ManageProgram : Page
    {
        #region page definition
        public const string PageKeyConst = "ProgramManagement.Manage";
        public const string PageDisplayConst = "Manage Program";
        public const string PageLinkConst = "/Modules/ProgramManagement/ManageProgram.aspx";
        public override string PageKey { get { return PageKeyConst; } }
        public override string PageDisplay { get { return PageDisplayConst; } }
        #endregion


        private Program Program { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Program = GetProgram(Request.QueryString["pid"] ?? "n/a");

            if (Program == null)
            {
                // could find player, redirect to find a player to manage
                Response.Redirect("/Modules/ProgramManagement/Default.aspx");
            }

            ProgramWizard.Program = Program;
        }

        private Program GetProgram(string programIdStr)
        {
            int programId;
            if (!int.TryParse(programIdStr, out programId)) return null;

            if (IsPostBack) return Session["ProgramManagement_Program"] as Program;

            using (ProgramsController c = new ProgramsController())
            {
                Session["ProgramManagement_Program"] = c.Get(programId);
                return Session["ProgramManagement_Program"] as Program;
            }
        }
    }
}