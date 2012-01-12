using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Application.FunctionalTests.Pages
{
    public class AccountLoginPage : PageBase
    {
        public AccountLoginPage(IWebDriver driver)
                : base(driver)
        {
            this.RelativeUrl = @"http://localhost:4544/Account/LogOn";
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "Password")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.Id, Using = @"a[href=""/Account/Register""]")]
        public IWebElement RegisterLink { get; set; }

        [FindsBy(How = How.Id, Using = "log-on-submit")]
        public IWebElement Submit { get; set; }

        [FindsBy(How = How.Id, Using = "UserName")]
        public IWebElement UserName { get; set; }

        public void LogOn(string userName, string password)
        {
            this.ThrowIfNotOnPage();

            this.UserName.SendKeys(userName);
            this.Password.SendKeys(password);
            this.Submit.Submit();
        }
    }
}