using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bandits
{
    public class LeftNavItem
    {
        public string Link { get; private set; }
        public string Name { get; private set; }
        public ISet<string> RequiredPermission { get; private set; }
        public string Glyphicon { get; private set; }

        public LeftNavItem(string Name, string Link, string Glyphicon, params string[] AllowedPermissions)
        {
            this.Name = Name;
            this.Link = Link;
            this.Glyphicon = Glyphicon;

            HashSet<string> _perms = new HashSet<string>();
            _perms.UnionWith(AllowedPermissions.Select(i => i));
            RequiredPermission = _perms;
        }
    }
}