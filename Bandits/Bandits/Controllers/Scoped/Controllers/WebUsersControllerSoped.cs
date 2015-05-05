using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bandits
{
    public class WebUsersControllerSoped : IScopable<WebUser, Int32>
    {
        public IQueryable<WebUser> GetScopedObjects(Auth_ScopeAssignment assignment)
        {
            ScopeType type = assignment.Auth_Scope.Scope;
            int resourceId = Convert.ToInt32(assignment.ResourceId);
            List<WebUser> scoped = new List<WebUser>();
            WebUserRepository repo = new WebUserRepository();

            switch (type)
            {
                case ScopeType.Client: //client level access
                    scoped.AddRange(repo.GetAll());
                    break;

                case ScopeType.ClubDepartment: // department level access
                    break;

                case ScopeType.Player: // player level access, the teams the player is on
                    break;

                case ScopeType.Program: // program level access
                    break;

                case ScopeType.Team: // team level access
                    break;

                default:
                    break;
            }

            return scoped.AsQueryable();
        }

        public IQueryable<int> GetScopedIds(Auth_ScopeAssignment assignment)
        {
            return GetScopedObjects(assignment).Select(i => i.WebUserId);
        }
    }
}