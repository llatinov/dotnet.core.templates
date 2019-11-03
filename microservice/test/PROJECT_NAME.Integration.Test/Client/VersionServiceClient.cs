using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PROJECT_NAME.Integration.Test.Client
{
    public class VersionServiceClient
    {
        private readonly HttpClient _httpClient;

        public VersionServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<string>> GetVersion()
        {
            var version = await GetAsync<string>($"/api/version");
            return version;
        }

        public async Task<ApiResponse<string>> GetVersionRestricted()
        {
            var version = await GetAsync<string>($"/api/version/restricted");
            return version;
        }

        private async Task<ApiResponse<T>> GetAsync<T>(string path)
        {
            var response = await _httpClient.GetAsync(path);
            var value = await response.Content.ReadAsStringAsync();
            var result = new ApiResponse<T>
            {
                StatusCode = response.StatusCode,
                ResultAsString = value
            };

            try
            {
                result.Result = JsonConvert.DeserializeObject<T>(value);
            }
            catch (Exception)
            {
                // Nothing to do
            }

            return result;
        }
    }
}