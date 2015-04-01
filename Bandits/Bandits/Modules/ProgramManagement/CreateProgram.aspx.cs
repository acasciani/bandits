using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bandits.Modules.ProgramManagement
{
    public partial class CreateProgram : Page
    {
        #region page definition
        public const string PageKeyConst = "ProgramManagement.CreateProgram";
        public const string PageDisplayConst = "Create Program";
        public const string PageLinkConst = "/Modules/ProgramManagement/CreateProgram.aspx";
        public override string PageKey { get { return PageKeyConst; } }
        public override string PageDisplay { get { return PageDisplayConst; } }
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }

            ProgramWizard.Program = ProgramCreation.Create();
        }

    }
}