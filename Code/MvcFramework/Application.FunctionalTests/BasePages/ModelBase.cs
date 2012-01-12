using System;
using System.Linq;
using DeleporterCore.Configuration;
using FrameworkTests.Pages;
using Application.FunctionalTests.DeleporterHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Application.FunctionalTests.BasePages
{
    /// <summary>
    ///   Underlying base class for PageModelBase and ControllerModel. Inherit from one of these classes rather than this one. Creates a Selenium model of a HTML page or pages
    /// </summary>
    public abstract class ModelBase
    {
        private const string ServerAppBaseUrl = "http://localhost";
        private LayoutPageModel _layout;

        public ModelBase(string relativeUrl) {
            this.RelativeUrl = relativeUrl;
        }

        [FindsBy(How = How.Id, Using = "error1")]
        public IWebElement FlashError1 { get; set; }

        [FindsBy(How = How.TagName, Using = "head")]
        public IWebElement Head { get; set; }

        public LayoutPageModel Layout {
            get {
                if (this._layout == null)
                    this._layout = new LayoutPageModel();
                return this._layout;
            }
        }

        /// <summary>
        ///   Url portion coming after http://localhost:1000/
        /// </summary>
        public string RelativeUrl { get; set; }

        protected static string ServerAppUrl { get { return ServerAppBaseUrl + ":" + DeleporterConfiguration.WebHostPort + "/"; } }
        protected IWebDriver Driver { get { return DriverFactory.Driver; } }

        public void ClearCookies() {
            this.Driver.Manage().Cookies.DeleteAllCookies();
        }

        /// <summary>
        ///   Is there a field with a validation error message containing this text.
        /// </summary>
        /// <param name="contains"> Text to search for </param>
        /// <returns> True if found </returns>
        public bool HasFieldValidationError(string contains) {
            var fieldValidationErrors = this.Driver.FindElements(By.ClassName("field-validation-error"));
            return fieldValidationErrors.Any(x => x.Text.Contains(contains));
        }

        /// <summary>
        ///   Does a validation summary error message exist and does it contain this text.
        /// </summary>
        /// <param name="contains"> Text to search for </param>
        /// <returns> True if found </returns>
        public bool HasFieldValidationSummaryError(string contains) {
            try {
                var validationSummaryErrors = this.Driver.FindElement(By.ClassName("validation-summary-errors"));
                return validationSummaryErrors.Text.Contains(contains);
            } catch (Exception) {
                return false;
            }
        }

        public bool HasFlashErrorMessage1() {
            var elements = this.Driver.FindElements(By.Id("error1"));
            return elements.Any();
        }

        public bool HasFlashErrorMessage1(string contains) {
            var elements = this.Driver.FindElements(By.Id("error1"));
            return elements.Any(x => x.Text.Contains(contains));
        }

        public bool HasFlashErrorMessage2(string contains) {
            var fieldValidationErrors = this.Driver.FindElements(By.Id("error2"));
            return fieldValidationErrors.Any(x => x.Text.Contains(contains));
        }

        public bool HasFlashInfoMessage1() {
            var elements = this.Driver.FindElements(By.Id("info1"));
            return elements.Any();
        }

        public bool HasValidationErrorSummary() {
            try {
                var validationSummaryErrors = this.Driver.FindElement(By.ClassName("validation-summary-errors"));
            } catch (Exception) {
                return false;
            }

            return true;
        }

        public void ThrowIfServerError() {
            var elements = this.Driver.FindElements(By.TagName("h1"));
            if (elements.Any(x => x.Text.Contains("Server Error")))
                throw new Exception("Server Error: \r\n" + this.Driver.FindElement(By.TagName("title")).Text + "\r\n"
                                    + this.Driver.PageSource);
        }
    }
}