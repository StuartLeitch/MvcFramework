using System.Linq;
using DeleporterCore.Client;
using DeleporterCore.Configuration;
using DeleporterCore.SelfHosting.SeleniumServer.Servers;
using DeleporterCore.SelfHosting.Servers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application.FunctionalTests.DeleporterHelpers;
using OpenQA.Selenium;

namespace Application.FunctionalTests
{
    [TestClass]
    public class TestBase
    {
        private const string ServerAppBaseUrl = "http://localhost";

        ///<summary>
        ///  Gets or sets the test context which provides information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        protected static IWebDriver Driver { get { return DriverFactory.Driver; } }
        protected static string ServerAppUrl { get { return ServerAppBaseUrl + ":" + DeleporterConfiguration.WebHostPort + "/"; } }

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

        [ClassCleanup]
        public static void ClassCleanup() {}

        [ClassInitialize]
        public static void ClassInit(TestContext testContext) {}

        [TestCleanup]
        public void MyTestCleanup() {
            Driver.Quit();

            // Runs any tidy up tasks in both the local and remote appdomains
            TidyupUtils.PerformTidyup();
            Deleporter.Run(TidyupUtils.PerformTidyup);
        }

        [TestInitialize]
        public void TestInitialize() {
            DriverFactory.GetNewDriverInstance();

            //DriverFactory.DriverGenerationMethod = () => new FirefoxDriver();
            //DriverFactory.DriverGenerationMethod = () => new ChromeDriver();
            //DriverFactory.DriverGenerationMethod = () => new InternetExplorerDriver();

            Deleporter.Run(() =>
                               {
                                   //var membershipMock = new Mock<IMembershipService>();
                                   //membershipMock.Setup(x => x.ValidateUser(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
                                   //membershipMock.Setup(x => x.GetUser(It.IsAny<string>())).Returns(new Person
                                   //{ UserName = "SomePerson", EmailAddress = "some@email.com", MemberID = 1 });
                                   //NinjectControllerFactoryUtils.TemporarilyReplaceBinding(membershipMock.Object);

                                   //// Throw by default on data access
                                   //var domainContextMock = new Mock<IDomainContext>(MockBehavior.Strict);
                                   //NinjectControllerFactoryUtils.TemporarilyReplaceBinding(domainContextMock.Object);
                               });
        }
    }
}