using flows_app.Dtos;
using flows_app.Entities;
using flows_app.Services;
using Microsoft.AspNetCore.Mvc;

namespace flows_app.Controllers
{
    [ApiController]
    [Route("api/fields")]
    public class FieldController : BaseController<Field, FieldRequest , FieldResponse>
    {
        public FieldController(IAsyncService<Field , FieldRequest , FieldResponse> service) : base(service) 
        {
  
        }
    }
}
