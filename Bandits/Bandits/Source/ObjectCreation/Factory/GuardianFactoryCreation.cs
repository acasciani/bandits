using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bandits.ObjectCreation
{
    public class GuardianFactoryCreation : IFactoryCreation<Guardian>
    {
        public Guardian AddNewObject(Guardian entity)
        {
            // Set creation values
            entity.CreateDate = DateTime.Now;

            using (GuardiansController c = new GuardiansController())
            {
                return c.AddNew(entity);
            }
        }

        public Guardian UpdateObject(Guardian entity)
        {
            // Set modify info
            entity.ModifyDate = DateTime.Now;

            using (GuardiansController c = new GuardiansController())
            {
                return c.Update(entity);
            }
        }
    }
}