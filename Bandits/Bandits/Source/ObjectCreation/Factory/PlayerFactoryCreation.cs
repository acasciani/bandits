using Bandits.Utils;
using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bandits.ObjectCreation
{
    public class PlayerFactoryCreation : IFactoryCreation<Player>
    {
        public Player AddNewObject(Player entity)
        {
            // Set creation values
            entity.CreateDate = DateTime.Now;
            entity.IsDeleted = false;

            using (PlayersController c = new PlayersController())
            {
                return c.AddNew(entity);
            }
        }
    }
}