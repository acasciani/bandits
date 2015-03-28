using Bandits.Pages;
using Bandits.Usability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bandits
{
    public abstract class MasterPage : System.Web.UI.MasterPage
    {
        public string ActiveLinkKey { get { return ((Page)Page).PageKey; } }

        public abstract string ModuleCacheKey { get; }
    }
}