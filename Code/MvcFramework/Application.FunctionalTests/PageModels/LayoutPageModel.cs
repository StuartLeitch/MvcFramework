using System;
using System.Linq;
using Application.FunctionalTests.BasePages;
using Application.FunctionalTests.DeleporterHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Application.FunctionalTests.PageModels
{
    /// <summary>
    ///   This class is exposed as a property on ModelBase
    /// </summary>
    public class LayoutPageModel : PageModelBase
    {
        public LayoutPageModel() : base("") {
            PageFactory.InitElements(DriverFactory.Driver, this);
        }

        [FindsBy(How = How.Id, Using = @"a[href=""/Home/About""]")]
        public IWebElement AboutLink { get; set; }

        [FindsBy(How = How.Id, Using = "Password")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.Id, Using = "log-on-submit")]
        public IWebElement Submit { get; set; }

        [FindsBy(How = How.Id, Using = "UserName")]
        public IWebElement UserName { get; set; }
		
        public override void Navigate() {
            throw new InvalidOperationException("Navigate does not make sense on layout.");
        }

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