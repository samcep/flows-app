using flows_app;
using flows_app.Entities;
using flows_app.Interfaces;
using flows_app.Services;

namespace flows_app.Services
{
    public interface IFlowRepository : IAsyncRepository<Flow> { }

    public class FlowRepository : AsyncRepository<Flow> , IFlowRepository
    {
        public FlowRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
