using flows_app.Dtos;
using flows_app.Entities;
using flows_app.Services;

namespace flows_app.Factories
{

    public abstract class StepHandler
    {
        protected StepHandler? _next;
        public void SetNext(StepHandler next) => _next = next;
        public abstract Task HandleStepAsync(FlowStepResponse step,
            HashSet<string> completedSteps,
            List<StepExecutionResult> stepExecutionResults,
            CancellationToken ct);
    }

    public class ConcreteStepHandler : StepHandler
    {
        private readonly IFlowService _flowService;
        public ConcreteStepHandler(IFlowService flowService)
        {
            _flowService = flowService;
        }
        public override async Task HandleStepAsync(FlowStepResponse step,
            HashSet<string> completedSteps, 
            List<StepExecutionResult> stepExecutionResults, 
            CancellationToken ct)
        {
            var dependenciesSteps = step.DependedBy.ToList();
            var canExecute = dependenciesSteps.All(d => completedSteps.Contains(d.DependsOnFlowStepId));

            if (canExecute)
            {
                var hasValidInputs = await _flowService.AreAllRequiredFieldsAvailableAsync(step.FlowStepFields.Select(f => f.Field.Id));
                if (hasValidInputs)
                {
                    await _flowService.MarkStepAsCompleted(step.Step.Id);
                    completedSteps.Add(step.Id);
                    stepExecutionResults.Add(new StepExecutionResult(step.Id, step.Step.Id, step.Step.Name, true, null));
                }
                else
                {
                }
            }

            if (_next is not null)
                await _next.HandleStepAsync(step, completedSteps , stepExecutionResults, ct);
        }
    }
    public class StepHandlerFactory
    {
        private readonly IFlowService _flowService;

        public StepHandlerFactory(IFlowService flowService)
        {
            _flowService = flowService;
        }
        public StepHandler BuildChain(IEnumerable<FlowStepResponse> dependentSteps)
        {
            StepHandler? first = null;
            StepHandler? current = null;

            foreach (var step in dependentSteps)
            {
                var handler = new ConcreteStepHandler(_flowService);
                if (first is null)
                {
                    first = handler;
                }
                else
                {
                    current?.SetNext(handler);
                }

                current = handler;
            }

            return first!;
        }

    }
}
