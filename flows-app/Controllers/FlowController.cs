using flows_app.Dtos;
using flows_app.Entities;
using flows_app.Factories;
using flows_app.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace flows_app.Controllers
{
    [ApiController]
    [Route("api/flows")]
    public class FlowController : BaseController<Flow, FlowRequest , FlowResponse>
    {
        private readonly IFlowService _flowService;
        public FlowController(IFlowService  service) : base(service) 
        {
            _flowService = service;
        }

        [HttpGet("{id}/execute")]
        public async Task<ActionResult> ExecuteFlow(string id, CancellationToken ct)
        {
            var flow = await _flowService.GetFlowAsync(id, ct);
            if (flow is null) return NotFound("Flow not found");

            var independentSteps = flow.FlowSteps.Where(fs => !fs.DependedBy.Any()).ToList();

            var executionTasks = independentSteps.Select(async step =>
            {
                try
                {
                    var hasValidInputs = await _flowService.AreAllRequiredFieldsAvailableAsync(
                        step.FlowStepFields.Select(fsf => fsf.Field.Id)
                    );

                    if (!hasValidInputs)
                    {
                        return new StepExecutionResult(step.Id , step.Step.Id, step.Step.Name, false, "Missing required fields");
                    }

                    await _flowService.MarkStepAsCompleted(step.Id);
                    return new StepExecutionResult(step.Id ,step.Step.Id, step.Step.Name, true, null);
                }
                catch (Exception ex)
                {
                    return new StepExecutionResult(step.Id  ,step.Step.Id, step.Step.Name, false, ex.Message);
                }
            });

            var stepResults = (await Task.WhenAll(executionTasks)).ToList();

            var dependentSteps = flow.FlowSteps.Where(fs => fs.DependedBy.Any());
            var factory = new StepHandlerFactory(_flowService);
            var chain = factory.BuildChain(dependentSteps);

            var completedSteps = new HashSet<string>(stepResults.Where(r => r.Success).Select(r => r.FlowStepId));
            await chain.HandleStepAsync(completedSteps, stepResults,  ct);
            var summary = new FlowExecutionSummary(
                FlowId: id,
                IsCompleted: stepResults.All(r => r.Success),
                Steps: stepResults
            );

            return Ok(summary);
        }
    }
}
