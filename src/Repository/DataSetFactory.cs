using System;
using Common.Repository;

namespace Repository
{
    public class DataSetFactory : IDataSetFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public DataSetFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IDataSet<TEntity> Resolve<TEntity>() where TEntity : class, IEntity
        {
            return (IDataSet<TEntity>)_serviceProvider.GetService(typeof(IDataSet<TEntity>));
        }

        public void Release<TEntity>(IDataSet<TEntity> item) where TEntity : class, IEntity
        {
            item = null;
        }
    }
}
