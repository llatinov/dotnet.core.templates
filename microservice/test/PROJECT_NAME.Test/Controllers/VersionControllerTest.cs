using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PROJECT_NAME.Controllers;
using PROJECT_NAME.Models;

namespace PROJECT_NAME.Test.Controllers
{
    [TestClass]
    public class VersionControllerTest
    {
        private VersionController _versionControllerUnderTest;
        private Mock<IOptions<AppConfig>> _optionsMock;
        private readonly AppConfig _appConfig = new AppConfig();

        [TestInitialize]
        public void Setup()
        {
            _optionsMock = new Mock<IOptions<AppConfig>>();
            _optionsMock.Setup(x => x.Value).Returns(_appConfig);
            _versionControllerUnderTest = new VersionController(_optionsMock.Object);
        }

        [TestMethod]
        public void GetVersion_ReturnsCorrectResult()
        {
            const string version = "2";
            _appConfig.Version = version;

            var result = _versionControllerUnderTest.Version();
            var asObjectResult = (ObjectResult) result;
            var asVersionDto = (VersionDto) asObjectResult.Value;

            Assert.AreEqual(version, asVersionDto.Version);
        }
    }
}
