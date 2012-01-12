using System;
using System.Linq;
using DeleporterCore.Configuration;
using OpenQA.Selenium;

namespace Application.FunctionalTests.Pages
{
    public class PageBase
    {
        protected void ThrowIfNotOnPage()
        {
            if(!this.IsOnPage){throw new InvalidOperationException("We are not currently on URL " + this.RelativeUrl + " which is required to use this page method");}
        }

        private readonly IWebDriver _driver;

        protected PageBase(IWebDriver driver) { this._driver = driver; }

        private const string ServerAppBaseUrl = "http://localhost";

        protected static string ServerAppUrl { get { return ServerAppBaseUrl + ":" + DeleporterConfiguration.WebHostPort + "/"; } }

        /// <summary>
        ///   Is the browser currently on this page
        /// </summary>
        public virtual bool IsOnPage { get { return this.Driver.Url.Contains(this.RelativeUrl); } }

        /// <summary>
        /// Url portion coming after http://localhost:1000/
        /// </summary>
        public string RelativeUrl { get; set; }

        protected IWebDriver Driver { get { return this._driver; } }

        public void ClearCookies() { this.Driver.Manage().Cookies.DeleteAllCookies(); }

        /// <summary>
        ///   Is there a field with a validation error message containing this text.
        /// </summary>
        /// <param name="contains"> Text to search for </param>
        /// <returns> True if found </returns>
        public bool HasFieldValidationError(string contains)
        {
            var fieldValidationErrors = this._driver.FindElements(By.ClassName("field-validation-error"));

            return fieldValidationErrors.Any(x => x.Text.Contains(contains));
        }

        /// <summary>
        ///   Does a validation summary error message exist and does it contain this text.
        /// </summary>
        /// <param name="contains"> Text to search for </param>
        /// <returns> True if found </returns>
        public bool HasFieldValidationSummaryError(string contains)
        {
            try {
                var validationSummaryErrors = this.Driver.FindElement(By.ClassName("validation-summary-errors"));
                return validationSummaryErrors.Text.Contains(contains);
            } catch (Exception) {
                return false;
            }
        }

        public bool HasValidationErrorSummary()
        {
            try {
                var validationSummaryErrors = this.Driver.FindElement(By.ClassName("validation-summary-errors"));
            } catch (Exception) {
                return false;
            }

            return true;
        }

        /// <summary>
        ///   Navigate to this page.
        /// </summary>
        public virtual void Navigate() { this.Driver.Navigate().GoToUrl(ServerAppUrl + this.RelativeUrl); }

    }
}