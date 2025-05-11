using flows_app.Dtos;
using flows_app.Entities;
using flows_app.Services;
using Microsoft.AspNetCore.Mvc;

namespace flows_app.Controllers
{
    [ApiController]
    [Route("api/flows")]
    public class FlowController : BaseController<Flow, FlowRequest , FlowResponse>
    {
        public FlowController(IAsyncService<Flow, FlowRequest, FlowResponse> service) : base(service) { }

        

    }
}
