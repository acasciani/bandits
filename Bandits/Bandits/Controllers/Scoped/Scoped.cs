using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bandits
{
    public class Scoped<T, U, Q>
        where T : IScopable<U, Q>, new() // The scope controller type
        where U : class, new() // The entity type
        where Q : struct // The primary key identifier
    {

        public IQueryable<U> GetScopedObjects()
        {
            WebUser user = UserManagement.GetCurrentWebUser();
            return GetScopedObjects(user);
        }

        public IQueryable<U> GetScopedObjects(Func<U, bool> where)
        {
            return GetScopedObjects().Where(i => where(i));
        }

        public IQueryable<U> GetScopedObjects(int userId)
        {
            WebUserRepository repo = new WebUserRepository();
            WebUser user = repo.GetBy(i => i.WebUserId == userId);
            return GetScopedObjects(user);
        }

        public IQueryable<U> GetScopedObjects(int userId, Func<U, bool> where)
        {
            return GetScopedObjects(userId).Where(i => where(i));
        }

        private IQueryable<U> GetScopedObjects(WebUser user)
        {
            // Get all scope assignments per user level and per role level
            IEnumerable<Auth_ScopeAssignment> assignments = user.ScopeAssignments.Union(user.RoleAssignments.SelectMany(r => r.ScopeAssignments));

            IEnumerable<Auth_ScopeAssignment> allowAssignments = assignments.Where(i => !i.Deny);
            IEnumerable<Auth_ScopeAssignment> denyAssignments = assignments.Where(i => i.Deny);

            T scoper = new T();
            IQueryable<U> scoped = Enumerable.Empty<U>().AsQueryable();

            foreach (Auth_ScopeAssignment assignment in allowAssignments)
            {
                scoped = scoped.Union(scoper.GetScopedObjects(assignment));
            }

            foreach (Auth_ScopeAssignment assignment in denyAssignments)
            {
                scoped = scoped.Except(scoper.GetScopedObjects(assignment));
            }

            return scoped;
        }
    }
}