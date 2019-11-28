using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PROJECT_NAME.Exceptions;

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
            return StatusCode((int)HttpStatusCode.OK, new { _config.Version });
        }

        [HttpGet("restricted")]
        public IActionResult VersionNotFound()
        {
            throw new HttpException(HttpStatusCode.Unauthorized);
        }
    }
}
