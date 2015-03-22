using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bandits.ObjectCreation
{
    public class AbstractFactoryCreation
    {
        public static IFactoryCreation<TEntity> GetFactory<TEntity>()
        {
            if (typeof(TEntity) == typeof(Person))
            {
                return (IFactoryCreation<TEntity>)new PersonFactoryCreation();
            }

            if (typeof(TEntity) == typeof(Guardian))
            {
                return (IFactoryCreation<TEntity>)new GuardianFactoryCreation();
            }

            if (typeof(TEntity) == typeof(Player))
            {
                return (IFactoryCreation<TEntity>)new PlayerFactoryCreation();
            }


            return null;
        }
    }
}