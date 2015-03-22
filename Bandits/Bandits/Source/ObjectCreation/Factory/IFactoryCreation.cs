using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bandits.ObjectCreation
{
    public interface IFactoryCreation<TEntity>
    {
        TEntity AddNewObject(TEntity entity);
    }
}