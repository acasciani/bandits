using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bandits.Usability
{
    public class PageRef
    {
        public string PageName { get; private set; }
        public string PageTitle { get; private set; }
        public string Location { get; private set; }
        public bool ActiveBreadcrumb { get; set; }

        public PageRef(string pageName, string pageTitle, string location)
        {
            PageName = pageName;
            PageTitle = pageTitle;
            Location = location;
            ActiveBreadcrumb = false;
        }
    }
}