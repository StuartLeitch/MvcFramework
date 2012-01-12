using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace MvcFramework.FunctionalTests.Pages
{
    public class LayoutPage : PageBase
    {
        public LayoutPage(IWebDriver driver) : base(driver)
        {
            this.RelativeUrl = @"http://localhost:4544/";
            PageFactory.InitElements(driver, this);
        }

        public override bool IsOnPage { get { throw new InvalidOperationException("Calling IsOnPage on LayoutPage does not make sense"); } }

        [FindsBy(How = How.Id, Using = "Password")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.Id, Using = @"a[href=""/""]")]
        public IWebElement HomeLink { get; set; }

        [FindsBy(How = How.Id, Using = @"a[href=""/Home/About""]")]
        public IWebElement AboutLink { get; set; }

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

        public override void Navigate() { throw new InvalidOperationException("Calling Navigate on LayoutPage does not make sense"); }
    }
}