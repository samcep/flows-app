using flows_app.Dtos;
using flows_app.Entities;

namespace flows_app.Services
{
    public class FlowService : AsyncService<Flow, FlowRequest, FlowResponse>
    {
        public FlowService(IAsyncRepository<Flow> repository) : base(repository) { }
        protected override Flow MapToEntity(FlowRequest dto)
        {
            return new Flow
            {
                Name = dto.Name
            };
        }
        protected override Flow MapToEntity(FlowRequest dto, Flow existing)
        {
            existing.Name = dto.Name;
            return existing;
        }

        protected override FlowResponse MapToResponse(Flow entity)
        {
            return new FlowResponse(entity.Id, entity.Name);
        }
        protected override FlowRequest MapToDto(Flow entity)
        {
            return new FlowRequest(entity.Name);
        }
    }
}
