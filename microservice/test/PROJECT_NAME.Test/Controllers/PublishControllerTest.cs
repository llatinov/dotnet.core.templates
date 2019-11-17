using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PROJECT_NAME.Controllers;
using PROJECT_NAME.Sqs;
using PROJECT_NAME.Sqs.Models;

namespace PROJECT_NAME.Test.Controllers
{
    [TestClass]
    public class PublishControllerTest
    {
        private PublishController _publishControllerUnderTest;
        private Mock<ISqsClient> _sqsClientMock;

        [TestInitialize]
        public void Setup()
        {
            _sqsClientMock = new Mock<ISqsClient>();
            _publishControllerUnderTest = new PublishController(_sqsClientMock.Object);
        }

        [TestMethod]
        public async Task PublishMovie_ReturnsCorrectResult()
        {
            var movie = new Movie();
            var result = await _publishControllerUnderTest.PublishMovie(movie);

            Assert.AreEqual(201, ((StatusCodeResult)result).StatusCode);
        }

        [TestMethod]
        public async Task PublishActor_ReturnsCorrectResult()
        {
            var actor = new Actor();
            var result = await _publishControllerUnderTest.PublishActor(actor);

            Assert.AreEqual(201, ((StatusCodeResult)result).StatusCode);
        }
    }
}
