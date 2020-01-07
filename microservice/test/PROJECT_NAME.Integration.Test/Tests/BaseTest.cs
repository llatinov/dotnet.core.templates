using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
//#if (AddSqsPublisher || AddSqsConsumer)
using Microsoft.Extensions.DependencyInjection;
using Moq;
//#endif
using PROJECT_NAME.Integration.Test.Client;
//#if (AddSqsConsumer)
using PROJECT_NAME.Services;
//#endif
//#if (AddSqsPublisher || AddSqsConsumer)
using PROJECT_NAME.Sqs;
//#endif
//#if (AddSerilog)
using Serilog;
//#endif

namespace PROJECT_NAME.Integration.Test.Tests
{
    public abstract class BaseTest
    {
        //#if (AddSqsPublisher || AddSqsConsumer)
        protected Mock<ISqsClient> SqsClientMock;
        //#endif
        //#if (AddSqsPublisher)
        protected PublishClient PublishClient;
        //#endif
        //#if (AddHealthChecks)
        protected HealthCheckClient HealthCheckClient;
        //#endif
        //#if (AddSqsConsumer)
        protected Mock<ISqsConsumerService> SqsConsumerServiceMock;
        //#endif
        protected VersionServiceClient VersionServiceClient;

        protected BaseTest()
        {
            //#if (AddSqsPublisher || AddSqsConsumer)
            SqsClientMock = new Mock<ISqsClient>();
            //#endif
            //#if (AddSqsConsumer)
            SqsConsumerServiceMock = new Mock<ISqsConsumerService>();
            //#endif

            var server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                //#if (AddSerilog)
                .UseSerilog()
                //#endif
                .ConfigureTestServices(services =>
                {
                    //#if (AddSqsPublisher || AddSqsConsumer)
                    services.AddSingleton(SqsClientMock.Object);
                    //#endif
                    //#if (AddSqsConsumer)
                    services.AddSingleton(SqsConsumerServiceMock.Object);
                    //#endif
                }));

            var httpClient = server.CreateClient();
            VersionServiceClient = new VersionServiceClient(httpClient);
            //#if (AddSqsPublisher)
            PublishClient = new PublishClient(httpClient);
            //#endif
            //#if (AddHealthChecks)
            HealthCheckClient = new HealthCheckClient(httpClient);
            //#endif
        }
    }
}