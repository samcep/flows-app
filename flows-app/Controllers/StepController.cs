
using flows_app.Dtos;
using flows_app.Entities;
using flows_app.Services;
using Microsoft.AspNetCore.Mvc;

namespace flows_app.Controllers
{
    [ApiController]
    [Route("api/steps")]
    public class StepController : BaseController<Step, StepRequest , StepResponse>
    {
        public StepController(IAsyncService<Step, StepRequest , StepResponse> service) : base(service)
        {

        }
    }
}
