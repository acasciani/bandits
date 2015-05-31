using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bandits.Modules.ProgramManagement
{
    public class LeftNav
    {
        private static IList<LeftNavItem> _Items;
        private const string PrefixLocation = "~/Modules/ProgramManagement/";

        public static IList<LeftNavItem> Items
        {
            get
            {
                Init();
                return _Items;
            }
        }

        private static void Init()
        {
            if (_Items == null)
            {
                _Items = new List<LeftNavItem>();
                _Items.Add(new LeftNavItem("Programs", PrefixLocation + "Programs/", "glyphicon glyphicon-th-list"));
                _Items.Add(new LeftNavItem("Teams", PrefixLocation + "Teams/", "glyphicon glyphicon-list"));
                _Items.Add(new LeftNavItem("Rosters", PrefixLocation + "Rosters/", "glyphicon glyphicon-paperclip"));
            }
        }
    }
}