using flows_app.Services;
using Microsoft.AspNetCore.Mvc;

namespace flows_app.Controllers
{
    public class BaseController<TEntity, TRequest , TResponse> : ControllerBase
    where TEntity : class, IEntity
    where TRequest : class
    {
        private readonly IAsyncService<TEntity, TRequest , TResponse> _service;
        public BaseController(IAsyncService<TEntity, TRequest , TResponse> service) => _service = service;

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                var dto = await _service.GetByIdAsync(id, cancellationToken);
                return Ok(dto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var dtos = await _service.GetAllAsync(cancellationToken);
            return Ok(dtos);
        }

        [HttpPost]
        public virtual async  Task<IActionResult> CreateAsync([FromBody] TRequest dto, CancellationToken cancellationToken)
        {
            var created = await _service.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = created }, created);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> UpdateAsync(string id, [FromBody] TRequest dto, CancellationToken cancellationToken)
        {
            try
            {
                var updated = await _service.UpdateAsync(id, dto, cancellationToken);
                return Ok(updated);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                await _service.DeleteAsync(id, cancellationToken);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
