using flows_app.Interfaces;

namespace flows_app.Entities
{

    public enum DirectionType
    {
        Input   =1,
        Output =2,
    }
    public class FlowStepField : IEntity
    {
        public string Id { get; set; }
        public string FlowStepId { get; set; }
        public FlowStep FlowStep { get; set; }
        public string FieldId { get; set; }
        public Field Field { get; set; }
        public DirectionType Direction { get; set; } 
    }

}
