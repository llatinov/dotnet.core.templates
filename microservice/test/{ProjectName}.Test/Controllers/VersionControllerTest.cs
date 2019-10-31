using {ProjectName}.Controllers;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace {ProjectName}.Test.Controllers
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

            Assert.AreEqual(version, result);
        }
    }
}
