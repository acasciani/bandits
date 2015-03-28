using Bandits.Usability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bandits.UI
{
    public partial class LeftNavigation : UserControl
    {
        public IList<LeftNavigationGroup> GroupsSource { get; set; }
        public string ActiveLinkKey { get; set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (GroupsSource != null)
            {
                Groups.DataSource = GroupsSource;
                Groups.DataBind();
            }
        }

        protected bool IsActive(string LinkKey)
        {
            return ActiveLinkKey == LinkKey;
        }
    }
}