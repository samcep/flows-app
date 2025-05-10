
using Microsoft.AspNetCore.Mvc;

namespace flows_app.Controllers
{
    [ApiController]
    [Route("api/steps")]
    public class StepController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Index()
        {
            return Ok("Hello from StepController");
        }
    }
}
