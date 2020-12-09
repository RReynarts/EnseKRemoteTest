using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Common.Repository;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    internal class DataSet<TEntity> : IDataSet<TEntity> where TEntity : class, IEntity
    {
        private readonly EnsekDbContext _dbContext;

        public DataSet(EnsekDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        
        public IQueryable<TEntity> Query()
        {
            return _dbContext.Set<TEntity>().AsQueryable();
        }

        public IQueryable<TEntity> QueryWithNoTracking()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate);
        }
        public IQueryable<TEntity> AsQueryable()
        {
            return _dbContext.Set<TEntity>().AsQueryable();
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return _dbContext.Set<TEntity>().AnyAsync(predicate, cancellationToken);
        }
        
        public TEntity Add(TEntity entity)
        {
            if (_dbContext.IsReadOnly)
                throw new NotSupportedException("In readonly state");

            return _dbContext.Set<TEntity>().Add(entity).Entity;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            if (_dbContext.IsReadOnly)
                throw new NotSupportedException("In readonly state");

            _dbContext.Set<TEntity>().AddRange(entities);
        }

        public TEntity Remove(TEntity entity)
        {
            if (_dbContext.IsReadOnly)
                throw new NotSupportedException("In readonly state");

            return _dbContext.Set<TEntity>().Remove(entity).Entity;
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            if (_dbContext.IsReadOnly)
                throw new NotSupportedException("In readonly state");

            _dbContext.Set<TEntity>().RemoveRange(entities);
        }
        
        public async Task<TEntity> FindAsync(params object[] keyValues)
        {
            return await _dbContext.Set<TEntity>().FindAsync(keyValues);
        }

        public TEntity Find(params object[] keyValues)
        {
            return _dbContext.Set<TEntity>().Find(keyValues);
        }

        public async Task<IEnumerable<TEntity>> BulkInsert(IEnumerable<TEntity> entities)
        {
            if (_dbContext.IsReadOnly)
                throw new NotSupportedException("In readonly state");
            _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
            _dbContext.ChangeTracker.AutoDetectChangesEnabled = true;

            return Query();
        }
    }
}