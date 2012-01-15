using System.Linq;
using Application.FunctionalTests.BasePages;
using Application.FunctionalTests.DeleporterHelpers;
using MvcFramework.Mvc.Controllers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Application.FunctionalTests.Pages
{
    public class AccountLoginModel : PageSeleniumModel<AccountController>
    {
        public AccountLoginModel() : base(x => x.LogOn()) {
            PageFactory.InitElements(DriverFactory.Driver, this);
        }

        [FindsBy(How = How.Id, Using = @"layoutRegisterLink")]
        public IWebElement RegisterLink { get; set; }

        [FindsBy(How = How.Id, Using = @"layoutLogonLink")]
        public IWebElement LogonLink { get; set; }

        [FindsBy(How = How.Id, Using = "Password")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.Id, Using = "log-on-submit")]
        public IWebElement Submit { get; set; }

        [FindsBy(How = How.Id, Using = "UserName")]
        public IWebElement UserName { get; set; }

        public void LogOn(string userName, string password) {
            this.ThrowIfNotOnPage();

            this.UserName.SendKeys(userName);
            this.Password.SendKeys(password);
            this.Submit.Submit();
        }
    }
}