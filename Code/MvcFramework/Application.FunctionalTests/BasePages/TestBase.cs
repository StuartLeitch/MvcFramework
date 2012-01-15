using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using DeleporterCore.Client;
using DeleporterCore.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application.FunctionalTests.DeleporterHelpers;
using OpenQA.Selenium;

namespace Application.FunctionalTests.BasePages
{
    /// <summary>
    ///   Typically use TestPageBase or TestControllerBase Base for actual MSTest test classes Spins up and shuts down Cassini & Selenium Server
    /// </summary>
    [TestClass]
    public abstract class TestBase
    {
        ///<summary>
        ///  Gets or sets the test context which provides information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// Provided for convenience.... typically access via Target.
        /// </summary>
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

        protected abstract void OnTestRunInitialize();
    }
}