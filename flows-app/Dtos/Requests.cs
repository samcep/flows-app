namespace flows_app.Dtos
{
    public sealed record FieldRequest(string Name);
    public sealed record FlowRequest(string Name);
    public sealed record StepRequest(string Name);
    public sealed record FieldResponse(string Id, string Name);
    public sealed record FlowResponse(string Id, string Name);
    public sealed record StepResponse(string Id  ,string Name);
    public sealed record FlowStepRequest(string FlowId , string StepId , int Order);
    public sealed record FlowStepResponse(string Id, string FlowId, string StepId, int Order);
}
