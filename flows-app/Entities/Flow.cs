using flows_app.Interfaces;

namespace flows_app.Entities
{
    public class Flow : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<FlowStep> FlowSteps { get; set; }
    }

}
