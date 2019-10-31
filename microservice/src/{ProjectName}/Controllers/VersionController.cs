using System.Net;
using {ProjectName}.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace {ProjectName}.Controllers
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
        public string Version()
        {
            return _config.Version;
        }

        [HttpGet("restricted")]
        public string VersionNotFound()
        {
            throw new HttpException(HttpStatusCode.Unauthorized);
        }
    }
}
