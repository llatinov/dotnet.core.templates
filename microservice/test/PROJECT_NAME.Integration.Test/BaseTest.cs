using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using PROJECT_NAME.Integration.Test.Client;

namespace PROJECT_NAME.Integration.Test
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