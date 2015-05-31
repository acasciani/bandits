using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bandits.Modules
{
    public class LeftNav
    {
        private static IList<LeftNavItem> _Items;
        private const string PrefixLocation = "~/Modules/";

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
                _Items.Add(new LeftNavItem("Programs", PrefixLocation + "ProgramManagement/", ""));
                _Items.Add(new LeftNavItem("Players", PrefixLocation + "PlayerManagement/", ""));
                _Items.Add(new LeftNavItem("Tournament", PrefixLocation + "TournamentManagement/", ""));
                _Items.Add(new LeftNavItem("Club", PrefixLocation + "ClubManagement/", ""));
            }
        }
    }
}