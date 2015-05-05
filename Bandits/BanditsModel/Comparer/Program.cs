using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanditsModel
{
    public partial class Program : IEquatable<Program>
    {
        public override int GetHashCode()
        {
            return ProgramId.GetHashCode();
        }

        public bool Equals(Program other)
        {
            if (other == null) { return false; }
            if (Object.ReferenceEquals(this, other)) { return true; }
            return ProgramId == other.ProgramId;
        }
    }
}
