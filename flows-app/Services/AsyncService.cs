namespace flows_app.Services
{
    public interface IAsyncService<TEntity, TRequest , TResponse> 
        where TEntity : class, IEntity
    {
        Task<IEnumerable<TResponse>> GetAllAsync(CancellationToken cancellationToken);
        Task<TResponse> GetByIdAsync(string id, CancellationToken cancellationToken);
        Task<TRequest> CreateAsync(TRequest dto, CancellationToken cancellationToken);
        Task<TRequest> UpdateAsync(string id, TRequest dto, CancellationToken cancellationToken);
        Task DeleteAsync(string id, CancellationToken cancellationToken);
    }
    public  abstract class AsyncService<TEntity, TRequest , TResponse> : IAsyncService<TEntity, TRequest , TResponse>
        where TEntity : class, IEntity
    {
        protected readonly IAsyncRepository<TEntity> _repository;
        public AsyncService(IAsyncRepository<TEntity> repository) => _repository = repository;

        public virtual async Task<IEnumerable<TResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync(cancellationToken);
            return entities.Select(MapToResponse);
        }
        public virtual async Task<TResponse> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException($"{typeof(TEntity).Name} with ID {id} not found.");

            return MapToResponse(entity);
        }

        public virtual async Task<TRequest> CreateAsync(TRequest dto, CancellationToken cancellationToken)
        {
            var entity = MapToEntity(dto);
            entity.Id = Guid.NewGuid().ToString();
            var created = await _repository.AddAsync(entity, cancellationToken);
            return MapToDto(created);
        }

        public virtual async Task<TRequest> UpdateAsync(string id, TRequest dto, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByIdAsync(id, cancellationToken);
            if (existing == null)
                throw new KeyNotFoundException($"{typeof(TEntity).Name} with ID {id} not found.");

            var updated = MapToEntity(dto, existing);
            var result = await _repository.UpdateAsync(updated, cancellationToken);
            return MapToDto(result);
        }

        public virtual async Task DeleteAsync(string id, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByIdAsync(id, cancellationToken);
            if (existing == null)
                throw new KeyNotFoundException($"{typeof(TEntity).Name} with ID {id} not found.");

            await _repository.DeleteAsync(existing, cancellationToken);
        }
        protected abstract TEntity MapToEntity(TRequest dto);
        protected abstract TEntity MapToEntity(TRequest dto, TEntity existing);
        protected abstract TResponse MapToResponse(TEntity existing);
        protected abstract TRequest MapToDto(TEntity entity);
    }


}
