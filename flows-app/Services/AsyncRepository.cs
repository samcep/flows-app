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
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TEntity>()
                .Where(condition)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TEntity>()
                .FindAsync(new object[] { id }, cancellationToken);
        }
        public async  Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
