using flows_app.Dtos;
using flows_app.Entities;
using flows_app.Repositories;
using Microsoft.EntityFrameworkCore;

namespace flows_app.Services
{
    public interface IFlowService : IAsyncService<Flow, FlowRequest, FlowResponse>
    {
        Task<FlowResponse> GetFlowAsync(string id, CancellationToken cancellationToken);
        Task<bool> AreAllRequiredFieldsAvailableAsync(IEnumerable<string> fieldIds);

        Task MarkStepAsCompleted(string stepId);
    }
    public class FlowService : AsyncService<Flow, FlowRequest, FlowResponse> , IFlowService
    {
        private readonly IFlowRepository _flowRepository;
        public FlowService(IFlowRepository repository) : base(repository) 
        {
            _flowRepository = repository;
        }
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
            return new FlowResponse(entity.Id, entity.Name , null);
        }
        protected override FlowRequest MapToDto(Flow entity)
        {
            return new FlowRequest(entity.Name);
        }

        public async Task<FlowResponse> GetFlowAsync(string id, CancellationToken cancellationToken)
        {
            var entity = await _flowRepository.GetFlowAsync(id, cancellationToken);
            return entity;
        }

        public async Task<bool> AreAllRequiredFieldsAvailableAsync(IEnumerable<string> fieldIds)
        {
            return await _flowRepository.AreAllRequiredFieldsAvailableAsync(fieldIds);
        }

        public async Task MarkStepAsCompleted(string stepId)
        {
            await _flowRepository.MarkStepAsCompleted(stepId);
        }
    }
}
