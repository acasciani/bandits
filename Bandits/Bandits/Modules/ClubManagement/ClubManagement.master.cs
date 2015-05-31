using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bandits.Modules.ClubManagement
{
    public partial class ClubManagement : MasterPage
    {
        public override IList<LeftNavItem> LeftNavContents { get { return LeftNav.Items; } }
    }
}