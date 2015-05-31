using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanditsModel
{
    public enum Gender { Undefined = 0, Male = 1, Female = 2 };

    public static class GenderTypeHelper
    {
        public static Gender GetGender(this char? refr)
        {
            if (refr.HasValue) return refr.Value.GetGender();
            return Gender.Undefined;
        }

        public static Gender GetGender(this char refr)
        {
            if (refr.Equals('M')) return Gender.Male;
            if (refr.Equals('F')) return Gender.Female;
            return Gender.Undefined;
        }

        public static Gender GetGender(this string refr)
        {
            if (!string.IsNullOrWhiteSpace(refr)) return refr.ToCharArray(0, 1)[0].GetGender();
            return Gender.Undefined;
        }

        public static string GetLabel(this Gender refr)
        {
            if (refr == Gender.Male) return "Male";
            if (refr == Gender.Female) return "Female";
            return "N/A";
        }
    }
}