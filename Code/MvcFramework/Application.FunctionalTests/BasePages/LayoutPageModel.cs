using System.Linq;
using Application.FunctionalTests.DeleporterHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Application.FunctionalTests.BasePages
{
    public partial class LayoutPageModel
    {
        public LayoutPageModel() {
            PageFactory.InitElements(DriverFactory.Driver, this);
        }

        [FindsBy(How = How.Id, Using = @"a[href=""/""]")]
        public IWebElement HomeLink { get; set; }
    }
}