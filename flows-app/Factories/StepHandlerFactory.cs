using flows_app.Dtos;
using flows_app.Entities;
using flows_app.Services;

namespace flows_app.Factories
{

    public abstract class StepHandler
    {
        protected StepHandler? _next;
        public void SetNext(StepHandler next) => _next = next;
        public abstract Task HandleStepAsync(
          HashSet<string> completedSteps,
          List<StepExecutionResult> stepExecutionResults,
          CancellationToken ct);
    }

    public class ConcreteStepHandler : StepHandler
    {
        private readonly IFlowService _flowService;
        private readonly FlowStepResponse _step;
        public ConcreteStepHandler(IFlowService flowService, FlowStepResponse step)
        {
            _flowService = flowService;
            _step = step;
        }
        public override async Task HandleStepAsync(HashSet<string> completedSteps,
           List<StepExecutionResult> stepExecutionResults,
           CancellationToken ct)
        {
            var dependenciesSteps = _step.DependedBy.ToList();
            var canExecute = dependenciesSteps.All(d => completedSteps.Contains(d.DependsOnFlowStepId));

            if (canExecute)
            {
                var hasValidInputs = await _flowService.AreAllRequiredFieldsAvailableAsync(_step.FlowStepFields.Select(f => f.Field.Id));
                if (hasValidInputs)
                {
                    await _flowService.MarkStepAsCompleted(_step.Id);
                    completedSteps.Add(_step.Id);
                    stepExecutionResults.Add(new StepExecutionResult(_step.Id, _step.Step.Id, _step.Step.Name, true, null));
                }
                else
                {
                    stepExecutionResults.Add(new StepExecutionResult(_step.Id, _step.Step.Id, _step.Step.Name, false, "All steps must be completed before executing this spte"));
                }
            }

            if (_next != null)
                await _next.HandleStepAsync(completedSteps, stepExecutionResults, ct);
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
            var handlerMap = dependentSteps.ToDictionary(
                step => step.Id,
                step => new ConcreteStepHandler(_flowService, step)
            );
            foreach (var step in dependentSteps)
            {
                var currentHandler = handlerMap[step.Id];

                foreach (var dependency in step.DependedBy)
                {
                    if (handlerMap.TryGetValue(dependency.DependsOnFlowStepId, out var previousHandler))
                    {
                        previousHandler.SetNext(currentHandler);
                    }
                }
            }
            return handlerMap[dependentSteps.First().Id];
        }

    }
}
