using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PROJECT_NAME.Integration.Test.Tests
{
    [TestClass]
    public class VersionTest : BaseTest
    {
        [TestMethod]
        public async Task GetVersion_ReturnsCorrectResult()
        {
            var response = await VersionServiceClient.GetVersion();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("1.0", response.Result.Version);
        }

        [TestMethod]
        public async Task GetVersionRestricted_ReturnsCorrectResult()
        {
            var response = await VersionServiceClient.GetVersionRestricted();

            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual(null, response.Result);
        }
    }
}
