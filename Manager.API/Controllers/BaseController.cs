using Microsoft.AspNetCore.Mvc;

namespace Manager.API.Controllers
{
    [ApiController]
    public class BaseController: ControllerBase
    {
        protected IActionResult WriteResponse(object result)
        {
            return Ok(result);
        }
    }
}
