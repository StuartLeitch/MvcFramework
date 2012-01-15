using System;
using System.Linq;
using System.Web.Mvc;
using Application.FunctionalTests.DeleporterHelpers;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Application.FunctionalTests.BasePages
{
    /// <summary>
    ///   This class is a property on ModelBase
    /// </summary>
    public class FlashModel
    {
        public FlashModel() {
            PageFactory.InitElements(DriverFactory.Driver, this);
        }

        // TODO Stuart: Complete Flash scenarios
        [FindsBy(How = How.Id, Using = "error1")]
        public IWebElement Error1 { get; set; }

        private IWebDriver Driver { get { return DriverFactory.Driver; } }

        private void AssertHasMessage(FlashType flashType, Number number, string contains)
        {
            var typeLocator = LocatorFromType(flashType);
            var elements = this.Driver.FindElements(By.Id(typeLocator + (int)number));
            var exists = string.IsNullOrWhiteSpace(contains) ? elements.Any() : elements.Any(x => x.Text.ToUpper().Contains(contains.ToUpper()));
            
            exists.Should().BeTrue("Message {0}{1} '{2}' should be present", typeLocator, (int)number, contains);
        }

        public void AssertHasAdditionalMessage(FlashType flashType = FlashType.Info)
        {
            this.AssertHasMessage(flashType, Number.Four, "ADDITIONAL MESSAGES EXIST BUT HAVE BEEN SUPPRESSED.");
        }

        private string LocatorFromType(FlashType flashType) {
            switch (flashType) {
                case FlashType.Info:
                    return FlashHelpers.Info;
                    case FlashType.Warning:
                    return FlashHelpers.Warning;
                    case FlashType.Error:
                    return FlashHelpers.Error;
            }

            throw new ArgumentException();            
        }

        public void AssertHasInfoMessage1(string contains = "") { this.AssertHasMessage(FlashType.Info, Number.One, contains); }
        public void AssertHasInfoMessage2(string contains = "") { this.AssertHasMessage(FlashType.Info, Number.Two, contains); }
        public void AssertHasInfoMessage3(string contains = "") { this.AssertHasMessage(FlashType.Info, Number.Three, contains); }
        public void AssertHasWarningMessage1(string contains = "") { this.AssertHasMessage(FlashType.Warning, Number.One, contains); }
        public void AssertHasWarningMessage2(string contains = "") { this.AssertHasMessage(FlashType.Warning, Number.Two, contains); }
        public void AssertHasWarningMessage3(string contains = "") { this.AssertHasMessage(FlashType.Warning, Number.Three, contains); }
        public void AssertHasErrorMessage1(string contains = "") { this.AssertHasMessage(FlashType.Error, Number.One, contains); }
        public void AssertHasErrorMessage2(string contains = "") { this.AssertHasMessage(FlashType.Error, Number.Two, contains); }
        public void AssertHasErrorMessage3(string contains = "") { this.AssertHasMessage(FlashType.Error, Number.Three, contains); }


    }

    public enum FlashType
    {
        Info,
        Warning, 
        Error,
    }

    public enum Number
    {
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
    }
}