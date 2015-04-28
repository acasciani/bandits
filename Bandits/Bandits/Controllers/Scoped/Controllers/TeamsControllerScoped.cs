using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bandits
{
    public class TeamsControllerScoped : IScopable<Team, Int32>{
        public IQueryable<Team> GetScopedObjects(Auth_ScopeAssignment assignment)
        {
            ScopeType type = assignment.Scope.Scope;
            int resourceId = Convert.ToInt32(assignment.ResourceId);
            List<Team> scoped = new List<Team>();
            TeamRepository repo = new TeamRepository();

            switch (type)
            {
                case ScopeType.Client: //client level access
                    scoped.AddRange(repo.GetAll());
                    break;

                case ScopeType.ClubDepartment: // department level access
                    break;

                case ScopeType.Player: // player level access, the teams the player is on
                    TeamPlayerRepository teamPlayer = new TeamPlayerRepository();
                    scoped.AddRange(teamPlayer.GetWhere(i => i.Player.PlayerId == resourceId).Select(i => i.Team));
                    break;

                case ScopeType.Program: // program level access
                    scoped.AddRange(repo.GetWhere(i => i.Program.ProgramId == resourceId));
                    break;

                case ScopeType.Team: // team level access
                    scoped.Add(repo.GetBy(i => i.TeamId == resourceId));
                    break;

                default:
                    break;
            }

            return scoped.AsQueryable();
        }

        public IQueryable<int> GetScopedIds(Auth_ScopeAssignment assignment)
        {
            return GetScopedObjects(assignment).Select(i => i.TeamId);
        }
    }
}