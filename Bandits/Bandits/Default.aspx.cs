using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bandits
{
    public partial class _Default : Page
    {
        public const string PageKeyConst = "Admin.Dashboard";
        public const string PageDisplayConst = "Dashboard";
        public const string PageLinkConst = "/";
        public override string PageKey { get { return PageKeyConst; } }
        public override string PageDisplay { get { return PageDisplayConst; } }


    }
}