using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
//#if (AddSqsPublisher || AddSqsConsumer)
using PROJECT_NAME.Sqs;
//#endif

namespace PROJECT_NAME.Integration.Test.Tests
{
    [TestClass]
    public class HealthCheckTest : BaseTest
    {
        [TestMethod]
        public async Task GetHealthReport_ReturnsHealthy()
        {
            //#if (AddSqsPublisher || AddSqsConsumer)
            SqsClientMock.Setup(x => x.GetQueueStatusAsync())
                .ReturnsAsync(new SqsStatus { IsHealthy = true });

            //#endif
            var response = await HealthCheckClient.GetHealth();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Healthy", response.Result.Status);
        }

        //#if (AddSqsPublisher || AddSqsConsumer)
        [TestMethod]
        public async Task GetHealthReport_ReturnsUnhealthy()
        {
            SqsClientMock.Setup(x => x.GetQueueStatusAsync())
                .ReturnsAsync(new SqsStatus { IsHealthy = false });

            var response = await HealthCheckClient.GetHealth();

            Assert.AreEqual(HttpStatusCode.ServiceUnavailable, response.StatusCode);
            Assert.AreEqual("Unhealthy", response.Result.Status);
        }
        //#endif
    }
}
