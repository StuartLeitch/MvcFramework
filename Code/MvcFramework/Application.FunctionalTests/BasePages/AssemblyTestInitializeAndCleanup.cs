using System.Linq;
using DeleporterCore.SelfHosting.SeleniumServer.Servers;
using DeleporterCore.SelfHosting.Servers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.FunctionalTests.BasePages
{
    /// <summary>
    ///   Best to keep these in their own class. Will run into issues with late binding if using in the generic TestBase
    /// </summary>
    [TestClass]
    public class AssemblyTestInitializeAndCleanup
    {
        [AssemblyCleanup]
        public static void AssemblyCleanup() {
            SeleniumServer.Instance.Stop();
            Cassini.Instance.Stop();
        }

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext testContext) {
            Cassini.Instance.Start();
            SeleniumServer.Instance.Start();
        }
    }
}