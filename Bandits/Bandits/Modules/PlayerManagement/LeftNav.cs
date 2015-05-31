using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bandits.Modules.PlayerManagement
{
    public class LeftNav
    {
        private static IList<LeftNavItem> _Items;
        private const string PrefixLocation = "~/Modules/PlayerManagement/";

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
                _Items.Add(new LeftNavItem("Players", PrefixLocation + "Players/", "glyphicon glyphicon-th-list"));
                _Items.Add(new LeftNavItem("Guardians", PrefixLocation + "Guardians/", "glyphicon glyphicon-info-sign"));
                _Items.Add(new LeftNavItem("Payments", PrefixLocation + "Payments/", "glyphicon glyphicon-credit-card"));
                _Items.Add(new LeftNavItem("Discipline", PrefixLocation + "Discipline/", "glyphicon glyphicon-fire"));
                _Items.Add(new LeftNavItem("Skills", PrefixLocation + "Skills/", "glyphicon glyphicon-stats"));
            }
        }
    }
}