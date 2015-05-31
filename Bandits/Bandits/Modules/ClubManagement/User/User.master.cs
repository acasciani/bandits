using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Bandits.Modules.ClubManagement.User
{
    public partial class User : MasterPage
    {
        public override IList<LeftNavItem> LeftNavContents
        {
            get { return null; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SetSelectedPage(Page as Page);
        }

        private void SetSelectedPage(Page page)
        {
            if (page == null) { return; }

            string key = page.PageKey;
            HtmlGenericControl activeControl;
            switch (key)
            {
                case "ViewUsers": activeControl = ViewUsers; break;
                case "AddUsers": activeControl = AddUsers; break;
                case "EditUsers": activeControl = EditUsers; break;
                default: activeControl = null; break;
            }

            if (activeControl != null)
            {
                activeControl.Attributes["class"] = "active";
            }
        }
    }
}