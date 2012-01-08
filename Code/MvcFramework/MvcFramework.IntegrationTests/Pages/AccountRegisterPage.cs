using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace MvcFramework.IntegrationTests.Pages
{
    public class AccountRegisterPage : PageBase
    {
        public AccountRegisterPage(IWebDriver driver)
                : base(driver)
        {
            this.Url = @"http://localhost:4544/Account/LogRegister";
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "Password")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.Id, Using = "ConfirmPassword")]
        public IWebElement ConfirmPassword { get; set; }

        [FindsBy(How = How.Id, Using = "Email")]
        public IWebElement Email { get; set; }

        [FindsBy(How = How.Id, Using = "log-on-submit")]
        public IWebElement Submit { get; set; }

        [FindsBy(How = How.Id, Using = "UserName")]
        public IWebElement UserName { get; set; }

        public void Register(string userName,string email, string password, string confirmationPassword)
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