using Bandits.Pages;
using Bandits.UI;
using Bandits.Usability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bandits
{
    public abstract class MasterPage : System.Web.UI.MasterPage
    {
        public abstract IList<LeftNavItem> LeftNavContents { get; }

        public virtual bool IsLocalTitleVisible { get; set; }
    }
}