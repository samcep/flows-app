using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace flows_app.Controllers
{
    [ApiController]
    [Route("api/flows")]
    public class FlowController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Index()
        {
            return Ok("Hello from FlowController");
        }
    }
}
