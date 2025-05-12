using flows_app.Entities;

namespace flows_app.Dtos
{
    public sealed record FieldRequest(string Name);
    public sealed record FlowRequest(string Name);
    public record FlowExecutionSummary(string FlowId, bool IsCompleted, List<StepExecutionResult> Steps);
    public sealed record StepRequest(string Name);

    public record StepExecutionResult(string FlowStepId , string StepId, string StepName, bool Success, string? ErrorMessage);
    public sealed record FlowStepRequest(string FlowId , string StepId , int Order);
    //Responses 

    public sealed record FlowResponse(
    string Id,
    string Name,
    List<FlowStepResponse> FlowSteps
);

    public sealed record FlowStepResponse(
        string Id,
        int Order,
        StepResponse Step,
        List<FlowStepFieldResponse> FlowStepFields,
        List<FlowStepDependencyResponse> DependedBy
    );

    public sealed record FlowStepFieldResponse(
        string Id,
        DirectionType Direction,
        FieldResponse Field
    );

    public sealed record FlowStepDependencyResponse(
        string Id,
        string DependsOnFlowStepId
    );

    public sealed record StepResponse(
        string Id,
        string Name
    );

    public sealed record FieldResponse(
        string Id,
        string Name
    );
}
