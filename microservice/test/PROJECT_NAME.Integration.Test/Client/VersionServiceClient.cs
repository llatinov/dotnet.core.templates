using System.Net.Http;
using System.Threading.Tasks;
using PROJECT_NAME.Models;

namespace PROJECT_NAME.Integration.Test.Client
{
    public class VersionServiceClient : BaseClient
    {
        public VersionServiceClient(HttpClient httpClient)
            : base(httpClient) { }

        public async Task<ApiResponse<VersionDto>> GetVersion()
        {
            var version = await SendAsync<VersionDto>(HttpMethod.Get, "api/version");
            return version;
        }

        public async Task<ApiResponse<string>> GetVersionRestricted()
        {
            var version = await SendAsync<string>(HttpMethod.Get, "api/version/restricted");
            return version;
        }
    }
}