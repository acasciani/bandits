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
        public override string ModuleCacheKey { get { return "ProgramManagement"; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

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
                LeftNavigationGroup ProgramGroup = new LeftNavigationGroup() { Items = new List<LeftNavigationItem>() };
                LeftNavigationGroup TeamGroup = new LeftNavigationGroup() { Items = new List<LeftNavigationItem>() };

                DashboardGroup.Items.Add(new LeftNavigationItem() { Key = Bandits._Default.PageKeyConst, Display = Bandits._Default.PageDisplayConst, Link = Bandits._Default.PageLinkConst });

                ProgramGroup.Items.Add(new LeftNavigationItem() { Key = Default.PageKeyConst, Display = Default.PageDisplayConst, Link = Default.PageLinkConst });
                ProgramGroup.Items.Add(new LeftNavigationItem() { Key = CreateProgram.PageKeyConst, Display = CreateProgram.PageDisplayConst, Link = CreateProgram.PageLinkConst });

                TeamGroup.Items.Add(new LeftNavigationItem() { Key = "ProgramManagement.AllTeams", Display = "All Teams", Link = "#" });
                TeamGroup.Items.Add(new LeftNavigationItem() { Key = "ProgramManagement.CreateTeam", Display = "Create Team", Link = "#" });

                leftNav = new LeftNavigationGroup[] { DashboardGroup, ProgramGroup, TeamGroup }.ToList();
                HttpContext.Current.Cache.Add(cacheKey, leftNav, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(12, 0, 0), System.Web.Caching.CacheItemPriority.Normal, null);
            }

            return leftNav;
        }
    }
}