using Bandits.Usability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bandits.Modules.ProgramManagement
{
    public partial class ProgramManagement : MasterPage
    {
        public override IList<LeftNavItem> LeftNavContents { get { return LeftNav.Items; } }
    }
}