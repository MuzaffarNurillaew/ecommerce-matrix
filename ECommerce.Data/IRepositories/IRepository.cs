namespace ECommerce.Data.IRepositories
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(Predicate<TEntity> predicate);
        Task<TEntity> SelectAsync(Predicate<TEntity> predicate);
        Task<List<TEntity>> SelectAllAsync(Predicate<TEntity> predicate = null);
    }
}