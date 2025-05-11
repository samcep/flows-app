using flows_app.Services;

namespace flows_app.Entities
{
    public class FlowStepDependency : IEntity
    {
        public string Id { get; set; }
        public string FlowStepId { get; set; }
        public FlowStep FlowStep { get; set; }
        public string DependsOnFlowStepId { get; set; }
        public FlowStep DependsOnFlowStep { get; set; }
    }
}
