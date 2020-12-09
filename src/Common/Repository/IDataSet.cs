using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Repository
{
    public interface IDataSet<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> Query();
        IQueryable<TEntity> QueryWithNoTracking();
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default);

        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> AsQueryable();
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        TEntity Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        Task<IEnumerable<TEntity>> BulkInsert(IEnumerable<TEntity> dataTable);
        Task<TEntity> FindAsync(params object[] keyValues);
        TEntity Find(params object[] keyValues);
        TEntity Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
