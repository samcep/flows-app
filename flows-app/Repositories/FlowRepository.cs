using flows_app.Entities;
using flows_app.Services;
using flows_app.Dtos;
using Microsoft.EntityFrameworkCore;


namespace flows_app.Repositories
{
    public interface IFlowRepository  : IAsyncRepository<Flow>
    {
        Task<FlowResponse> GetFlowAsync(string id, CancellationToken cancellationToken);
        Task<bool> AreAllRequiredFieldsAvailableAsync(IEnumerable<string> fieldIds);

        Task MarkStepAsCompleted(string stepId);
    }
    public class FlowRepository : AsyncRepository<Flow> , IFlowRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public FlowRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }
        public async Task<FlowResponse> GetFlowAsync(string id, CancellationToken cancellationToken)
        {
            var flow = await _dbContext.Flows
                .Include(f => f.FlowSteps)
                    .ThenInclude(fs => fs.Step)
                .Include(f => f.FlowSteps)
                    .ThenInclude(fs => fs.FlowStepFields)
                        .ThenInclude(fsf => fsf.Field)
                .Include(f => f.FlowSteps)
                    .ThenInclude(fs => fs.DependedBy)
                .FirstOrDefaultAsync(f => f.Id == id, cancellationToken);

            if (flow == null) return null;

            return new FlowResponse(
                Id: flow.Id,
                Name: flow.Name,
                FlowSteps: flow.FlowSteps.Select(fs => new FlowStepResponse(
                    Id: fs.Id,
                    Order: fs.Order,
                    Step: new StepResponse(
                        Id: fs.Step?.Id,
                        Name: fs.Step?.Name
                    ),
                    FlowStepFields: fs.FlowStepFields?.Select(fsf => new FlowStepFieldResponse(
                        Id: fsf.Id,
                        Direction: fsf?.Direction ?? DirectionType.Input,
                        Field: new FieldResponse(
                            Id: fsf.Field?.Id,
                            Name: fsf.Field?.Name
                        )
                    )).ToList() ?? new(),
                    DependedBy: fs.DependedBy?.Select(dep => new FlowStepDependencyResponse(
                        Id: dep.Id,
                        DependsOnFlowStepId: dep.DependsOnFlowStepId
                    )).ToList() ?? new()
                )).ToList()
            );
        }
        

        public async Task<bool> AreAllRequiredFieldsAvailableAsync(IEnumerable<string> fieldIds)
        {
            var existingFieldIds = await _dbContext.Fields
                .Where(f => fieldIds.Contains(f.Id))
                .Select(f => f.Id)
                .ToListAsync();
            return existingFieldIds.Count == fieldIds.Count();
        }

        public async Task MarkStepAsCompleted(string flowStepId)
        {
            var step = await _dbContext.FlowSteps.FirstOrDefaultAsync(s => s.Id == flowStepId);

            if (step is null)
                throw new InvalidOperationException($"Step with ID '{flowStepId}' not found.");

            step.IsCompleted = true;
            await _dbContext.SaveChangesAsync();
        }
    }   
}
