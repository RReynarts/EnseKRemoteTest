namespace Common.Repository
{
    public interface IDataSetFactory
    {
        IDataSet<TEntity> Resolve<TEntity>() where TEntity : class, IEntity;
        void Release<TEntity>(IDataSet<TEntity> item) where TEntity : class, IEntity;
    }
}
