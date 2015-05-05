using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.OpenAccess;

namespace Bandits
{
    public class Scoped<T, U, Q>
        where T : IScopable<U, Q>, new() // The scope controller type
        where U : class, new() // The entity type
        where Q : struct // The primary key identifier
    {

        public IQueryable<U> GetScopedObjects(string permission)
        {
            WebUser user = UserManagement.GetCurrentWebUser();
            return GetScopedObjects_Local(user.WebUserId, permission);
        }

        public IQueryable<U> GetScopedObjects(string permission, Func<U, bool> where)
        {
            return GetScopedObjects(permission).Where(i => where(i));
        }

        public IQueryable<U> GetScopedObjects(int userId, string permission)
        {
            return GetScopedObjects_Local(userId, permission);
        }

        public IQueryable<U> GetScopedObjects(int userId, string permission, Func<U, bool> where)
        {
            return GetScopedObjects(userId, permission).Where(i => where(i));
        }

        private IQueryable<U> GetScopedObjects_Local(int userId, string permission)
        {
            if (string.IsNullOrWhiteSpace(permission))
            {
                throw new ArgumentNullException("Permission was null, empty or whitespace. A permission must be specified.");
            }
            BanditsModel.BanditsModel model = new BanditsModel.BanditsModel();
            permission = permission.ToLower();
            WebUser user;
            IQueryable<WebUser> results = model.WebUsers.Where(i => i.WebUserId == userId).Include(i => i.Auth_Assignments);

            if(results == null || results.Count() != 1){
                return null;
            }

            user=results.First();
             IQueryable<Auth_Assignment> assignments = user.Auth_Assignments.AsQueryable().Include(i=>i.Permission).Include(i=>i.Role).Include(i=>i.Auth_ScopeAssignments);

            // Get all scope assignments per user level and per role level
            IEnumerable<Auth_ScopeAssignment> individualSAssignments = assignments.Where(i => i.IsExplicit && i.Permission.PermissionName.ToLower() == permission).SelectMany(i => i.Auth_ScopeAssignments);
            IEnumerable<Auth_ScopeAssignment> roleSAssignments = assignments.Where(i=> !i.IsExplicit && i.Role.Permissions.Select(k=>k.PermissionName.ToLower()).Contains(permission)).SelectMany(i=>i.Auth_ScopeAssignments);
            IEnumerable<Auth_ScopeAssignment> allowAssignments = individualSAssignments.Where(i => !i.Deny).Union(roleSAssignments.Where(i => !i.Deny));
            IEnumerable<Auth_ScopeAssignment> denyAssignments = individualSAssignments.Where(i => i.Deny).Union(roleSAssignments.Where(i => i.Deny));

            IEnumerable<U> scoped = new List<U>();
            T scoper = new T();
            foreach (Auth_ScopeAssignment assignment in allowAssignments)
            {
                scoped = scoped.Union(scoper.GetScopedObjects(assignment));
            }

            foreach (Auth_ScopeAssignment assignment in denyAssignments)
            {
                scoped = scoped.Except(scoper.GetScopedObjects(assignment));
            }

            model.Dispose();

            return scoped.Distinct().AsQueryable();
        }
    }
}