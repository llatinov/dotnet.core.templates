using Newtonsoft.Json;

namespace PROJECT_NAME.Models
{
    public class VersionDto
    {
        [JsonProperty("version")]
        public string Version { get; set; }
    }
}