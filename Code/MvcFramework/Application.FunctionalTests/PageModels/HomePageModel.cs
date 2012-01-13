using Application.FunctionalTests.BasePages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace FrameworkTests.Pages
{
    public class HomePageModel : PageModelBase
    {
        public HomePageModel() : base("") {}

        [FindsBy(How = How.TagName, Using = @"p")]
        public IWebElement Paragraph { get; set; }

    }
}