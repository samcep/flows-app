using System.Linq.Expressions;

namespace flows_app.Interfaces
{
    public interface IEntity
    {
        public string Id { get; set; }
    }
    public interface IAsyncRepository<TEntity> where TEntity : class
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> condition = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeString = null,
            bool disableTracking = true);
        Task<IReadOnlyCollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> condition = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            bool disableTracking = true);
        Task<TEntity> GetByIdAsync(int Id , CancellationToken cancellationToken);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(TEntity entity);
    }
}
