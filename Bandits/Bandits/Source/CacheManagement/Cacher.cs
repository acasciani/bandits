using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace Bandits.Source.CacheManagement
{
    public class Cacher
    {
        public IEnumerable<object> GetCached<T, Q>(int userID) 
            where Q : class, new()
            where T : IScopable<Q, Int32>, new()
        {
            string key = typeof(T).FullName; // namespace and class name

            Scoped<T, Q, Int32> scoper = new Scoped<T, Q, int>();
            
            scoper.GetScopedObjects(userID, "");

        }


        private IEnumerable<T> GetMasterCache<T>()
            where T:class
        {
            return HttpContext.Current.Cache.Get(key) as IEnumerable<T>;
        }

        private void UpdateMasterCache<T>()
            where T:class
        {
        }

        private string GetCacheKey<T>()
            where T : class
        {
            return typeof(T).FullName; // namespace + class name
        }
    }
}