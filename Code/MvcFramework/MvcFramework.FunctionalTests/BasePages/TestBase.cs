using System.Linq;
using DeleporterCore.Client;
using DeleporterCore.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcFramework.FunctionalTests.DeleporterHelpers;
using OpenQA.Selenium;

namespace MvcFramework.FunctionalTests.BasePages
{
    /// <summary>
    ///   Typically use TestPageBase or TestControllerBase Base for actual MSTest test classes Spins up and shuts down Cassini & Selenium Server
    /// </summary>
    [TestClass]
    public abstract class TestBase
    {
        private const string ServerAppBaseUrl = "http://localhost";

        /// <summary>
        ///   The URL for the base of the application ie. http://localhost:8080/
        /// </summary>
        public static string ServerAppUrl { get { return ServerAppBaseUrl + ":" + DeleporterConfiguration.WebHostPort + "/"; } }

        ///<summary>
        ///  Gets or sets the test context which provides information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        protected static IWebDriver Driver { get { return DriverFactory.Driver; } }

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
            this.OnTestRunInitialize();
        }

        /// <summary>
        ///   Instantiate TestPage here. Selenium.Support seems to have issues when PageFactory.InitElements is called via generics. Manual workaround here. Implementation like this.Target = new SomethingControllerModel(); Area of friction to resolve. (Selenium Support Page Object chokes if created via Reflection.
        /// </summary>
        protected abstract void OnTestRunInitialize();
    }
}