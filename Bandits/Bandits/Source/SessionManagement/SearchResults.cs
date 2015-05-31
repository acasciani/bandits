using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bandits.SessionManagement
{
    public static class SearchResults<T> where T: class
    {
        // Only want to store search results in a Results session so we don't store a lot of data. used for searching/sorting/paging etc.
        public static void SetSession(List<T> results)
        {
            HttpContext.Current.Session["Results"] = results;
        }

        public static List<T> GetSession()
        {
            return HttpContext.Current.Session["Results"] as List<T>;
        }
    }
}