using Bandits.Utils;
using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bandits.Modules.ProgramManagement
{
    public partial class Default : Page
    {
        #region page definition
        public const string PageKeyConst = "ProgramManagement.Default";
        public const string PageDisplayConst = "All Programs";
        public const string PageLinkConst = "/Modules/ProgramManagement/Default.aspx";
        public override string PageKey { get { return PageKeyConst; } }
        public override string PageDisplay { get { return PageDisplayConst; } }
        #endregion


        protected class ProgramSortable
        {
            public string ProgramName { get; set; }
            public Program ProgramObject { get; set; }
        }

        private IEnumerable<Program> Programs
        {
            get
            {
                using (ProgramsController c = new ProgramsController())
                {
                    return c.Get();
                }
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            AllowRole("Admin");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }

            BindProgramsResults();
        }

        private IEnumerable<ProgramSortable> ProgramsSortable(IEnumerable<Program> data)
        {
            return data.Select(p => new ProgramSortable() { ProgramName = p.Name, ProgramObject = p });
        }

        private void BindProgramsResults()
        {
            ProgramsResults.DataSource = ProgramsSortable(Programs);
            ProgramsResults.DataBind();
        }

        protected void ProgramsResults_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortHelper<ProgramSortable>.Sort(ProgramsSortable(Programs), GetSortDirection(e.SortExpression.ToString()) == "ASC", e.SortExpression.ToString(), ref ProgramsResults);
        }

        protected void ProgramsResults_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int programId;
            if (!int.TryParse(e.CommandArgument.ToString(), out programId))
            {
                return;
            }

            switch (e.CommandName)
            {
                case "Edit":
                    Response.Redirect("~/Modules/ProgramManagement/ManageProgram.aspx?pid=" + programId);
                    break;
                default: break;
            }
        }
    }
}