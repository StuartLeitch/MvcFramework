using System;
using System.Threading;
using System.Web.Mvc;
using System.Web.Security;
using Application.Biz.Data.EntityFramework;
using Application.FunctionalTests.BasePages;
using Application.FunctionalTests.DeleporterHelpers;
using Application.FunctionalTests.Pages;
using DeleporterCore.Client;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcFramework.Mvc.Controllers;
using MvcFramework.Mvc.Models;
using OpenQA.Selenium.Firefox;

namespace FrameworkTests.PageTests
{
    [TestClass]
    public class AccountRegisterTests : TestBase
    {
        private AccountRegisterModel Target { get; set; }

        [TestMethod]
        public void AttemptLogon_SomethingHappens() {
            TestSetupHelper.StubRegisterTestUser();

            Target.Register("test", "test@user.com", "password", "password");
            Console.WriteLine(Target.Layout.LoginDisplayName.Text);
            Target.Layout.LoginDisplayName.Text.ToLower().Should().Be("test");
            Console.WriteLine(this.Target.Driver.Url);
            Target.Layout.IsLoggedOn().Should().BeTrue();
        }

        [TestMethod]
        public void NotLoggedOn_ShouldNotDisplayUserWelcome() {
            Target.Layout.IsLoggedOn().Should().BeFalse();
        }

        protected override void OnTestRunInitialize() {
           // DriverFactory.DriverGenerationMethod = () => new FirefoxDriver();
           this.Target = new AccountRegisterModel();
           this.Target.Navigate();
        }

    }
}