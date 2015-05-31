using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bandits.Modules.ClubManagement
{
    public class LeftNav
    {
        private static IList<LeftNavItem> _Items;

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
                _Items.Add(new LeftNavItem("Users", "~/Modules/ClubManagement/Users/", "glyphicon glyphicon-user", Permissions.DeleteUser, Permissions.UpsertUser, Permissions.ViewUser));
                _Items.Add(new LeftNavItem("Scoping", "~/Modules/ClubManagement/Scoping/", "glyphicon glyphicon-eye-open", Permissions.DeleteUser, Permissions.UpsertUser, Permissions.ViewUser));
            }
        }
    }
}