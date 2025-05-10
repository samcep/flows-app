using flows_app.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace flows_app.Services
{
    public class AsyncRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _dbContext;

        public AsyncRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<TEntity>()
                .Where(condition)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> condition = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeString = null,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (disableTracking)
                query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString))
                query = query.Include(includeString);

            if (condition != null)
                query = query.Where(condition);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyCollection<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> condition = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (disableTracking)
                query = query.AsNoTracking();

            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            if (condition != null)
                query = query.Where(condition);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }

}
