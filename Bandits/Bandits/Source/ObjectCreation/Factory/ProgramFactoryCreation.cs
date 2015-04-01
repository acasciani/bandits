using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bandits.ObjectCreation
{
    public class ProgramFactoryCreation : IFactoryCreation<Program>
    {
        public Program AddNewObject(Program entity)
        {
            // Set creation values

            using (ProgramsController c = new ProgramsController())
            {
                return c.AddNew(entity);
            }
        }

        public Program UpdateObject(Program entity)
        {
            // Set modify info

            using (ProgramsController c = new ProgramsController())
            {
                return c.Update(entity);
            }
        }
    }
}