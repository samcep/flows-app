using flows_app.Entities;
using flows_app.Interfaces;

namespace flows_app.Services
{
    public interface IStepRepository : IAsyncRepository<Step> { }
    public class StepRepository : AsyncRepository<Step>, IStepRepository
    {
        public StepRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
