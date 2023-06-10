// MicroserviceTwo/Controllers/EchoController.cs

using Microsoft.AspNetCore.Mvc;

namespace MicroserviceTwo.Controllers
{
    [ApiController]
    [Route("api/echo")]
    public class EchoController : ControllerBase
    {
        [HttpPost]
        public IActionResult Echo([FromBody] string message)
        {
            return Ok(message);
        }
    }
}