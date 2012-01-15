using System.Linq;
using Application.FunctionalTests.DeleporterHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Application.FunctionalTests.BasePages
{
    /// <summary>
    ///   This class is a property on SeleniumModel
    /// </summary>
    public class LayoutModel
    {
        public LayoutModel() {
            PageFactory.InitElements(DriverFactory.Driver, this);
        }

        [FindsBy(How = How.TagName, Using = "head")]
        public IWebElement Head { get; set; }

        [FindsBy(How = How.Id, Using = @"userFriendlyName")]
        public IWebElement UserFriendlyName { get; set; }

        [FindsBy(How = How.Id, Using = @"a[href=""/Account/Register""]")]
        public IWebElement RegisterLink { get; set; }

        [FindsBy(How = How.Id, Using = @"a[href=""/Home/About""]")]
        public IWebElement AboutLink { get; set; }

        [FindsBy(How = How.Id, Using = "Password")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.Id, Using = "log-on-submit")]
        public IWebElement Submit { get; set; }

        [FindsBy(How = How.Id, Using = "UserName")]
        public IWebElement UserName { get; set; }

        [FindsBy(How = How.Id, Using = @"loginUserFriendlyName")]
        public IWebElement LoginDisplayName { get; set; }

        public bool IsLoggedOn() {
            var driver = DriverFactory.Driver;
            var test = DriverFactory.Driver.PageSource;
            return TestHelpers.ElementExistsById("loginUserFriendlyName");
        }

        public void LogOn(string userName, string password)
        {
            this.UserName.SendKeys(userName);
            this.Password.SendKeys(password);
            this.Submit.Submit();
        }

        // TODO Stuart: IsLoggedOn
        // TODO Stuart: LogOn
        // TODO Stuart: LogOff

    }
}