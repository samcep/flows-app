using Microsoft.AspNetCore.Mvc;

namespace flows_app.Controllers
{
    [ApiController]
    [Route("api/fields")]
    public class FieldController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Index()
        {
            return Ok("Hello from FieldController");
        }
    }
}
