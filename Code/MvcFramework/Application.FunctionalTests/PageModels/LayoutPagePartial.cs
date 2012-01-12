using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace FrameworkTests.Pages
{
    /// <summary>
    ///   This class is a property on ModelBase
    /// </summary>
    public partial class LayoutPageModel
    {
        [FindsBy(How = How.Id, Using = @"a[href=""/Home/About""]")]
        public IWebElement AboutLink { get; set; }

        [FindsBy(How = How.Id, Using = "Password")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.Id, Using = "log-on-submit")]
        public IWebElement Submit { get; set; }

        [FindsBy(How = How.Id, Using = "UserName")]
        public IWebElement UserName { get; set; }

        // TODO Stuart: IsLoggedOn
        // TODO Stuart: LogOn
        // TODO Stuart: LogOff

        //public void LogOn(string userName, string password)
        //{
        //    this.ThrowIfNotOnPage();
        //    this.UserName.SendKeys(userName);
        //    this.Password.SendKeys(password);
        //    this.Submit.Submit();
        //}
    }
}