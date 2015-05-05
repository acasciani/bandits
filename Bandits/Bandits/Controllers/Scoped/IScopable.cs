using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bandits
{
    public interface IScopable<T, Q>
        where T : class, new()
        where Q : struct
    {
        /// <summary>Gets all scoped objects for passed in user id</summary>
        IQueryable<T> GetScopedObjects(Auth_ScopeAssignment assignment);

        IQueryable<Q> GetScopedIds(Auth_ScopeAssignment assignment);
    }
}
