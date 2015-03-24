using Bandits.Utils;
using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanditsModel
{
    public static class PlayerCreation
    {
        public static Player Create()
        {
            return new Player() { CreateDate = DateTime.Now, IsDeleted = false }.AsPerson(PersonCreation.Create()).HasGuardian(GuardianCreation.Create());
        }

        public static Player AsPerson(this Player refr, Person person) { refr.Person = person; return refr; }

        public static Player HasGuardians(this Player refr, params Guardian[] guardians)
        {
            foreach (Guardian guardian in guardians)
            {
                // We don't want to add the same guardian more than once
                if (refr.Guardians.Where(g => g.GuardianId == guardian.GuardianId).Count() == 0)
                {
                    // this guardian is not added, it's ok to add them now.
                    refr.Guardians.Add(guardian);
                }
            }
            return refr;
        }

        public static Player HasGuardian(this Player refr, Guardian guardian)
        {
            refr.Guardians.Add(guardian);
            return refr;
        }
    }
}