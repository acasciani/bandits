using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bandits.Usability
{
    public class LeftNavigationGroup
    {
        public IList<LeftNavigationItem> Items { get; set; }
    }

    public class LeftNavigationItem
    {
        public string Key { get; set; }
        public string Display { get; set; }
        public string Link { get; set; }
    }
}