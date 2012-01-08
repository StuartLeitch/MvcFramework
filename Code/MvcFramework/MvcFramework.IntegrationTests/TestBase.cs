using System;
using System.Linq;
using DeleporterCore.Client;
using DeleporterCore.Configuration;
using DeleporterCore.SelfHosting.SeleniumServer.Servers;
using DeleporterCore.SelfHosting.Servers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcFramework.IntegrationTests.DeleporterHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace MvcFramework.IntegrationTests
{
    [TestClass]
    public class TestBase
    {
        private const string ServerAppBaseUrl = "http://localhost";

        ///<summary>
        ///  Gets or sets the test context which provides information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        protected static IWebDriver Driver { get; private set; }
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
            Driver.Dispose();
            Driver = null;

            // Runs any tidy up tasks in both the local and remote appdomains
            TidyupUtils.PerformTidyup();
            Deleporter.Run(TidyupUtils.PerformTidyup);
        }

        [TestInitialize]
        public void TestInitialize() {
            Driver = new RemoteWebDriver(new Uri("http://127.0.0.1:4444/wd/hub"), DesiredCapabilities.HtmlUnitWithJavaScript());

            // Driver = new FirefoxDriver();
            // Driver = new ChromeDriver();
            // Driver = new InternetExplorerDriver();

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

        protected void LogOn(IWebDriver Driver) {
            Driver.Navigate().GoToUrl(ServerAppUrl);
            var query = Driver.FindElement(By.Name("UserName"));
            query.SendKeys("SomeUsername");
            query = Driver.FindElement(By.Name("Password"));
            query.SendKeys("SomePassword");

            Driver.FindElement(By.Name("log-on")).Click();
        }

        //private const string RelativePathToServerApp = "..\\..\\..\\Olli.Mvc";

        //private static string TestServerRootDirectory
        //{
        //    get
        //    {
        //        var currentAssemblyCodeBase = Assembly.GetExecutingAssembly().CodeBase;
        //        var currentAssemblyDirectory =
        //                Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(currentAssemblyCodeBase).Path));

        //        var testServerRootDirectory = Path.GetFullPath(Path.Combine(currentAssemblyDirectory, RelativePathToServerApp));

        //        return testServerRootDirectory;
        //    }
        //}

        //private static void RestartServer()
        //{
        //    using (var wc = new WebClient())
        //    {
        //        var html = wc.DownloadString(ServerAppUrl);
        //        if (!html.Contains(ServerAppExpectedHtml))
        //        {
        //            throw new Exception("Can't continue with tests: the test app isn't running at " + ServerAppUrl);
        //        }
        //    }
        //}

        //private static void RecycleServerAppDomain()
        //{
        //    var webConfigFileName = Path.Combine(TestServerRootDirectory, "Web.config");

        //    File.SetLastWriteTime(webConfigFileName, DateTime.Now);
        //}

        //private static void HostSeleniumServerIfNeeded()
        //{
        //    try
        //    {
        //        new RemoteWebDriver(new Uri("http://127.0.0.1:4444/wd/hub"), DesiredCapabilities.HtmlUnitWithJavaScript());
        //    }
        //    catch (Exception)
        //    {
        //        _seleniumProcess = new Process
        //             {
        //                 StartInfo =
        //                         {
        //                             FileName = @"C:\Program Files\Java\jre7\bin\java.exe",
        //                             Arguments = @"-jar E:\Installed\selenium-server-standalone-2.15.0.jar",
        //                             WindowStyle = ProcessWindowStyle.Hidden,
        //                             UseShellExecute = false,
        //                             RedirectStandardOutput = true,
        //                             RedirectStandardInput = true,
        //                             CreateNoWindow = true,
        //                         }
        //             };

        //        _seleniumProcess.Start();
        //    }
        //}

        //private static void StartIisExpressIfNeeded()
        //{
        //    try
        //    {
        //        new WebClient().DownloadString(ServerAppUrl);
        //    }
        //    catch (Exception)
        //    {
        //        _iisExpressProcess =
        //        new Process
        //            {
        //                StartInfo =
        //                        {
        //                            FileName = "\"C:\\Program Files (x86)\\IIS Express\\iisexpress.exe\"",
        //                            Arguments =
        //                                    "/path:\"" + TestServerRootDirectory + "\" /port:" + ServerAppPort + " /trace:error",
        //                            WindowStyle = ProcessWindowStyle.Hidden,
        //                            UseShellExecute = false,
        //                            RedirectStandardOutput = true,
        //                            RedirectStandardInput = true,
        //                            CreateNoWindow = true,
        //                        }
        //            };

        //        _iisExpressProcess.Start();
        //    }
        //}

        //[AssemblyCleanup]
        //public static void AssemblyCleanup()
        //{
        //    //if (_iisExpressProcess != null)
        //    //{
        //    //    _iisExpressProcess.Kill();
        //    //}
        //    //if (_seleniumProcess != null)
        //    //{
        //    //    _seleniumProcess.Kill();
        //    //}
        //}

        //[AssemblyInitialize]
        //public static void AssemblyInit(TestContext testContext)
        //{
        //    //StartIisExpressIfNeeded();
        //    //HostSeleniumServerIfNeeded();
        //    ThrowIfSeleniumOrIISNotRunning();

        //    RecycleServerAppDomain();
        //    RestartServer();
        //}
    }
}