using flows_app.Services;
namespace flows_app.Entities
{
    public class FlowStep : IEntity
    {
        public string Id { get; set; }
        public string FlowId { get; set; }
        public Flow Flow { get; set; }
        public string StepId { get; set; }
        public Step Step { get; set; }
        public int Order { get; set; }
        public ICollection<FlowStepField> FlowStepFields { get; set; }
        public ICollection<FlowStepDependency> DependedBy { get; set; }
    }
}
