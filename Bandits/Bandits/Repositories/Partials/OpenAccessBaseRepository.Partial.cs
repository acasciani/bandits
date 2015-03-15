using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Telerik.OpenAccess;
using Telerik.OpenAccess.FetchOptimization;

namespace Bandits
{
    public partial interface IOpenAccessBaseRepository<TEntity, TContext> where TContext : OpenAccessContext, new()
    {
        IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> filter);
        IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> filter, params Expression<Func<object, IEnumerable<object>>>[] loadWith);
    }

    public abstract partial class OpenAccessBaseRepository<TEntity, TContext> : IOpenAccessBaseRepository<TEntity, TContext> where TContext : OpenAccessContext, new()
    {
        public virtual IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null) throw new ArgumentNullException("filter");

            List<TEntity> detachedEntities = dataContext.CreateDetachedCopy<List<TEntity>>(GetAllEntities(filter), fetchStrategy);
            return detachedEntities.AsQueryable();
        }

        public virtual IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> filter, params Expression<Func<object, IEnumerable<object>>>[] loadWith)
        {
            FetchStrategy fetch = new FetchStrategy();
            fetch.LoadWith(loadWith);

            List<TEntity> detachedEntities = dataContext.CreateDetachedCopy<List<TEntity>>(GetAllEntities(filter), fetchStrategy);
            return detachedEntities.AsQueryable();
        }

        private List<TEntity> GetAllEntities(Expression<Func<TEntity, bool>> filter)
        {
            return dataContext.GetAll<TEntity>().Where<TEntity>(filter).ToList<TEntity>();
        }
    }
}