using System.Linq;
using Application.FunctionalTests.BasePages;
using Application.FunctionalTests.DeleporterHelpers;
using MvcFramework.Mvc.Controllers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Application.FunctionalTests.Pages
{
    public class AccountRegisterModel : PageSeleniumModel<AccountController>
    {
        public AccountRegisterModel() : base(x => x.Register()) {
            PageFactory.InitElements(DriverFactory.Driver, this);
        }

        [FindsBy(How = How.Id, Using = @"UserName")]
        public IWebElement UserName { get; set; }

        [FindsBy(How = How.Id, Using = @"Email")]
        public IWebElement Email { get; set; }

        [FindsBy(How = How.Id, Using = @"Password")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.Id, Using = @"ConfirmPassword")]
        public IWebElement ConfirmPassword { get; set; }

        [FindsBy(How = How.Id, Using = @"register-main-submit")]
        public IWebElement Submit { get; set; }

        public void Register(string userName, string email, string password, string confirmationPassword)
        {
            this.ThrowIfNotOnPage();
            this.UserName.SendKeys(userName);
            this.Email.SendKeys(email);
            this.Password.SendKeys(password);
            this.ConfirmPassword.SendKeys(confirmationPassword);
            this.Submit.Submit();
        }
    }
}