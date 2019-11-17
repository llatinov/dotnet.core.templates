using System.Net.Http;
using System.Threading.Tasks;

namespace PROJECT_NAME.Integration.Test.Client
{
    public class VersionServiceClient : BaseClient
    {
        public VersionServiceClient(HttpClient httpClient)
            : base(httpClient) { }

        public async Task<ApiResponse<string>> GetVersion()
        {
            var version = await SendAsync<string>(HttpMethod.Get, "api/version");
            return version;
        }

        public async Task<ApiResponse<string>> GetVersionRestricted()
        {
            var version = await SendAsync<string>(HttpMethod.Get, "api/version/restricted");
            return version;
        }
    }
}