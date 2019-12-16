using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PROJECT_NAME.Exceptions;
using PROJECT_NAME.Models;

namespace PROJECT_NAME.Controllers
{
    [Route("api/[controller]")]
    public class VersionController : Controller
    {
        private readonly AppConfig _config;

        public VersionController(IOptions<AppConfig> options)
        {
            _config = options.Value;
        }

        [HttpGet]
        public IActionResult Version()
        {
            var result = new VersionDto { Version = _config.Version };
            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [HttpGet("restricted")]
        public IActionResult VersionNotFound()
        {
            throw new HttpException(HttpStatusCode.Unauthorized);
        }
    }
}
