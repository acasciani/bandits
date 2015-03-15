using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bandits.Utils
{
    public static class LinqExtensions
    {
        public static bool In(this string amI, params string[] inThisList)
        {
            return inThisList.Contains(amI);
        }
    }
}