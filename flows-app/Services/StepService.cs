using flows_app.Dtos;
using flows_app.Entities;

namespace flows_app.Services
{
    public class StepService : AsyncService<Step, StepRequest, StepResponse>
    {
        public StepService(IAsyncRepository<Step> repository) : base(repository) { }
        protected override Step MapToEntity(StepRequest dto)
        {
            return new Step
            {
                Name = dto.Name
            };
        }
        protected override Step MapToEntity(StepRequest dto, Step existing)
        {
            existing.Name = dto.Name;
            return existing;
        }

        protected override StepResponse MapToResponse(Step entity)
        {
            return new StepResponse(entity.Id, entity.Name);
        }
        protected override StepRequest MapToDto(Step entity)
        {
            return new StepRequest(entity.Name);
        }
    }
}
