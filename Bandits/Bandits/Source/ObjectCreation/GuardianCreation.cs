using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bandits;

namespace BanditsModel
{
    public static class GuardianCreation
    {
        public static Guardian Create()
        {
            return new Guardian() { CreateDate = DateTime.Now }.AsPerson(PersonCreation.Create());
        }

        public static Guardian AsPerson(this Guardian refr, Person person) { refr.Person = person; return refr; }

        public static Guardian WithGuardianType(this Guardian refr, GuardianType type) { refr.GuardianType = type; return refr; }

        public static Guardian WithGuardianType(this Guardian refr, int typeID)
        {
            using (GuardianTypesController c = new GuardianTypesController())
            {
                return refr.WithGuardianType(c.Get(typeID));
            }
        }

        public static Guardian WithGuardianType(this Guardian refr, string typeID)
        {
            int typeIDint;
            if (int.TryParse(typeID, out typeIDint))
            {
                return refr.WithGuardianType(typeIDint);
            }
            return refr;
        }
    }
}