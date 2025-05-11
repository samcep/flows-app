using flows_app.Dtos;
using flows_app.Entities;
namespace flows_app.Services
{
    public class FieldService : AsyncService<Field, FieldRequest, FieldResponse>
    {
        public FieldService(IAsyncRepository<Field> repository) : base(repository) { }
        protected override Field MapToEntity(FieldRequest dto)
        {
            return new Field
            {
                Name = dto.Name
            };
        }
        protected override Field MapToEntity(FieldRequest dto, Field existing)
        {
            existing.Name = dto.Name;
            return existing;
        }

        protected override FieldResponse MapToResponse(Field entity)
        {
            return new FieldResponse(entity.Id, entity.Name);
        }
        protected override FieldRequest MapToDto(Field entity)
        {
            return new FieldRequest(entity.Name);
        }
    }

}
