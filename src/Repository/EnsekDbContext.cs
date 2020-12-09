using System;
using System.Reflection;
using System.Threading.Tasks;
using Common.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Repository
{
    public class EnsekDbContext : DbContext, IUnitOfWork
    {
        public bool IsReadOnly { get; private set; }

        public EnsekDbContext(DbContextOptions<EnsekDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        
        public IUnitOfWorkTransaction AsReadOnly()
        {
            IsReadOnly = true;
            return new UnitOfWorkTransaction(this, IsReadOnly, true);
        }

        public IUnitOfWorkTransaction BeginTransaction(bool isLongRunning)
        {
            IsReadOnly = false;
            return new UnitOfWorkTransaction(this, IsReadOnly, isLongRunning);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public void AutoDetectChanges(bool on)
        {
            this.ChangeTracker.AutoDetectChangesEnabled = on;
        }
        
        private sealed class UnitOfWorkTransaction : IUnitOfWorkTransaction
        {
            private readonly EnsekDbContext _dataModelContext;
            private readonly IDbContextTransaction _transaction;
            private readonly bool _readOnly;

            public UnitOfWorkTransaction(EnsekDbContext dataModelContext, bool readOnly, bool isLongRunning)
            {
                _dataModelContext = dataModelContext ?? throw new ArgumentNullException(nameof(dataModelContext));
                _readOnly = readOnly;
                if (isLongRunning) return;
                if (_dataModelContext.Database.IsRelational())
                {
                    _transaction = _dataModelContext.Database.BeginTransaction();
                }
            }

            public async Task CompletedAsync()
            {
                if (_readOnly) return;
                _dataModelContext.ChangeTracker.DetectChanges();
                await _dataModelContext.SaveChangesAsync();
                if (_transaction != null) await _transaction?.CommitAsync();
            }

            #region IDisposable
            private bool _disposed;

            public void Dispose()
            {
                Dispose(true);

                // Call SupressFinalize in case a subclass implements a finalizer.
                GC.SuppressFinalize(this);
            }

            private void Dispose(bool disposing)
            {
                if (_disposed) return;

                // If you need thread safety, use a lock around these  
                // operations, as well as in your methods that use the resource. 
                if (disposing)
                {
                    _transaction?.Dispose();
                }

                // Indicate that the instance has been disposed.
                _disposed = true;
            }
            #endregion

        }
    }
}
