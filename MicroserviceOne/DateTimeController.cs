// MicroserviceOne/Controllers/DateTimeController.cs

using System;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceOne.Controllers
{
    [ApiController]
    [Route("api/datetime")]
    public class DateTimeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetDateTime

()
        {
            var dateTime = DateTime.Now;
            return Ok(dateTime);
        }
    }
}