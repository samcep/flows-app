using System.Linq.Expressions;

namespace flows_app.Interfaces
{
    public interface IAsyncRepository<TEntity> where TEntity : class
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> condition = null, CancellationToken cancellationToken = default);
        Task<TEntity> GetByIdAsync(int Id , CancellationToken cancellationToken);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(TEntity entity);
    }
}
