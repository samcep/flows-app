using flows_app.Entities;
using flows_app.Interfaces;

namespace flows_app.Services
{
    public interface IFlowStepRepository : IAsyncRepository<FlowStep> { }
    public class FlowStepRepository : AsyncRepository<FlowStep>, IFlowStepRepository
    {
        public FlowStepRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }

}
