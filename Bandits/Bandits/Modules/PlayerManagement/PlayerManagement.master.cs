using Bandits.Usability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bandits.UI;

namespace Bandits.Modules.PlayerManagement
{
    public partial class PlayerManagement : MasterPage
    {
        public override IList<LeftNavItem> LeftNavContents { get { return LeftNav.Items; } }
    }
}