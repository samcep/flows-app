﻿using flows_app.Services;

namespace flows_app.Entities
{
    public class Field : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<FlowStepField> FlowStepFields { get; set; }
    }
}
