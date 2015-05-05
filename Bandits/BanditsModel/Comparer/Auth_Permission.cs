using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanditsModel
{
    public partial class Auth_Permission
    {
        public override bool Equals(object obj)
        {
            Auth_Permission to = (Auth_Permission)obj;
            return this.PermissionId == to.PermissionId;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
