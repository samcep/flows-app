using flows_app.Entities;
using flows_app.Interfaces;

namespace flows_app.Services
{
    public interface IFieldRepository : IAsyncRepository<Field> { }

    public class FieldRepository : AsyncRepository<Field>, IFieldRepository
    {
        public FieldRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }

}
