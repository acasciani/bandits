using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bandits
{
    public class ProgramsControllerScoped : IScopable<Program, Int32>
    {
        public IQueryable<Program> GetScopedObjects(Auth_ScopeAssignment assignment)
        {
            ScopeType type = assignment.Scope.Scope;
            int resourceId = Convert.ToInt32(assignment.ResourceId);
            List<Program> scoped = new List<Program>();
            
            ProgramRepository repo = new ProgramRepository();

            switch (type)
            {
                case ScopeType.Client: //client level access
                    scoped.AddRange(repo.GetAll());
                    break;

                case ScopeType.ClubDepartment: // department level access
                    break;

                case ScopeType.Player: // player level access, only has access to the program of the team(s) they belong to
                    TeamPlayerRepository teamPlayer = new TeamPlayerRepository();
                    scoped.AddRange(teamPlayer.GetWhere(i => i.Player.PlayerId == resourceId).Select(i => i.Team.Program));
                    break;

                case ScopeType.Program: // program level access
                    scoped.Add(repo.GetBy(i=>i.ProgramId == resourceId));
                    break;

                case ScopeType.Team: // team level access, only have access to the program they belong in
                    TeamRepository team = new TeamRepository();
                    scoped.Add(team.GetBy(i => i.TeamId == resourceId).Program);
                    break;

                default:
                    break;
            }

            return scoped.AsQueryable();
        }

        public IQueryable<int> GetScopedIds(Auth_ScopeAssignment assignment)
        {
            return GetScopedObjects(assignment).Select(i => i.ProgramId);
        }
    }
}