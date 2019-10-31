using {ProjectName}.Integration.Test.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace {ProjectName}.Integration.Test
{
    public abstract class BaseTest
    {
        protected VersionServiceClient VersionServiceClient;

        public BaseTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureServices(services =>
                {
                }));

            var httpClient = server.CreateClient();
            VersionServiceClient = new VersionServiceClient(httpClient);
        }
    }
}