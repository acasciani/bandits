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
        public override string ModuleCacheKey { get { return "PlayerManagement"; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            LeftNav.ActiveLinkKey = ActiveLinkKey == null ? "" : ActiveLinkKey;
            LeftNav.GroupsSource = BindLeftNavigation();
            LeftNav.DataBind();
        }

        private IList<LeftNavigationGroup> BindLeftNavigation()
        {
            string cacheKey = "LeftNavigation" + ModuleCacheKey;

            IList<LeftNavigationGroup> leftNav = HttpContext.Current.Cache.Get(cacheKey) as IList<LeftNavigationGroup>;

            if (leftNav == null)
            {
                LeftNavigationGroup DashboardGroup = new LeftNavigationGroup() { Items = new List<LeftNavigationItem>() };
                LeftNavigationGroup PlayerManagementGroup = new LeftNavigationGroup() { Items = new List<LeftNavigationItem>() };
                LeftNavigationGroup PlayerReportsGroup = new LeftNavigationGroup() { Items = new List<LeftNavigationItem>() };

                DashboardGroup.Items.Add(new LeftNavigationItem() { Key = Bandits._Default.PageKeyConst, Display = Bandits._Default.PageDisplayConst, Link = Bandits._Default.PageLinkConst });

                PlayerManagementGroup.Items.Add(new LeftNavigationItem() { Key = Default.PageKeyConst, Display = Default.PageDisplayConst, Link = Default.PageLinkConst });
                PlayerManagementGroup.Items.Add(new LeftNavigationItem() { Key = Create.PageKeyConst, Display = Create.PageDisplayConst, Link = Create.PageLinkConst });
                PlayerManagementGroup.Items.Add(new LeftNavigationItem() { Key = "PlayerManagement.ManagePlayer", Display = "Manage Player", Link = "#" });
                PlayerManagementGroup.Items.Add(new LeftNavigationItem() { Key = "PlayerManagement.ContactPlayer", Display = "Contact Player", Link = "#" });

                PlayerReportsGroup.Items.Add(new LeftNavigationItem() { Key = "PlayerManagement.Reports.Payments", Display = "Payments", Link = "#" });
                PlayerReportsGroup.Items.Add(new LeftNavigationItem() { Key = "PlayerManagement.Reports.Disciplinary", Display = "Disciplinary", Link = "#" });
                PlayerReportsGroup.Items.Add(new LeftNavigationItem() { Key = "PlayerManagement.Reports.SkillsAssessment", Display = "Skills Assessment", Link = "#" });

                leftNav = new LeftNavigationGroup[] { DashboardGroup, PlayerManagementGroup, PlayerReportsGroup }.ToList();
                HttpContext.Current.Cache.Add(cacheKey, leftNav, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(12, 0, 0), System.Web.Caching.CacheItemPriority.Normal, null);
            }

            return leftNav;
        }


    }
}