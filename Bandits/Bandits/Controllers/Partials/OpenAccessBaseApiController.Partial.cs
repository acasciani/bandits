using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Http;
using Telerik.OpenAccess;

namespace Bandits
{
    public abstract partial class OpenAccessBaseApiController<TEntity, TContext> : ApiController where TContext : OpenAccessContext, new()
    {
        public virtual IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> filter)
        {
            var allEntities = repository.GetWhere(filter);
            return allEntities;
        }

        public virtual IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> filter, params Expression<Func<object, IEnumerable<object>>>[] loadWith)
        {
            var allEntities = repository.GetWhere(filter, loadWith);
            return allEntities;
        }

        public virtual TEntity GetBy(Expression<Func<TEntity, bool>> filter)
        {
            return repository.GetBy(filter);
        }

        public virtual TEntity AddNew(TEntity entity)
        {
            return repository.AddNew(entity);
        }

        public virtual TEntity Update(TEntity entity)
        {
            return repository.Update(entity);
        }
    }
}