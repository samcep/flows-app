using flows_app.Entities;
using flows_app.Interfaces;

namespace flows_app.Services
{
    public interface IFlowStepFieldRepository : IAsyncRepository<FlowStepField> { }

    public class FlowStepFieldRepository : AsyncRepository<FlowStepField>, IFlowStepFieldRepository
    {
        public FlowStepFieldRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
