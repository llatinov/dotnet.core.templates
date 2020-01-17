using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PROJECT_NAME.Controllers;
using PROJECT_NAME.Services;
using PROJECT_NAME.Sqs;

namespace PROJECT_NAME.Test.Controllers
{
    [TestClass]
    public class ConsumerControllerTest
    {
        private ConsumerController _consumerControllerUnderTest;
        private Mock<ISqsConsumerService> _consumerServiceMock;

        [TestInitialize]
        public void Setup()
        {
            _consumerServiceMock = new Mock<ISqsConsumerService>();
            _consumerControllerUnderTest = new ConsumerController(_consumerServiceMock.Object);
        }

        [TestCleanup]
        public void TearDown()
        {
            _consumerServiceMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void Start_ReturnsCorrectResult_AndCallsCorrectMethod()
        {
            var result = _consumerControllerUnderTest.Start();
            var asObjectResult = (StatusCodeResult)result;

            Assert.AreEqual(200, asObjectResult.StatusCode);
            _consumerServiceMock.Verify(x => x.StartConsuming());
        }

        [TestMethod]
        public void Stop_ReturnsCorrectResult_AndCallsCorrectMethod()
        {
            var result = _consumerControllerUnderTest.Stop();
            var asObjectResult = (StatusCodeResult)result;

            Assert.AreEqual(200, asObjectResult.StatusCode);
            _consumerServiceMock.Verify(x => x.StopConsuming());
        }

        [TestMethod]
        public async Task Reprocess_ReturnsCorrectResult_AndCallsCorrectMethod()
        {
            var result = await _consumerControllerUnderTest.Reprocess();
            var asObjectResult = (StatusCodeResult)result;

            Assert.AreEqual(200, asObjectResult.StatusCode);
            _consumerServiceMock.Verify(x => x.ReprocessMessagesAsync());
        }

        [TestMethod]
        public async Task Status_ReturnsCorrectResult_AndCallsCorrectMethod()
        {
            var sqsStatus = new SqsStatus();
            _consumerServiceMock.Setup(x => x.GetStatusAsync())
                .ReturnsAsync(sqsStatus);

            var result = await _consumerControllerUnderTest.Status();
            var asObjectResult = (ObjectResult)result;
            var asObjectValue = (SqsStatus)asObjectResult.Value;

            Assert.AreEqual(200, asObjectResult.StatusCode);
            Assert.AreEqual(sqsStatus, asObjectValue);
            _consumerServiceMock.Verify(x => x.GetStatusAsync());
        }
    }
}
