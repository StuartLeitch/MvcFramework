using System;
using System.Linq;
using Application.FunctionalTests.DeleporterHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Application.FunctionalTests.BasePages
{
    /// <summary>
    ///   This class is a property on SeleniumModel
    /// </summary>
    public class ValidationModel
    {
        public ValidationModel() {
            PageFactory.InitElements(DriverFactory.Driver, this);
        }

        protected IWebDriver Driver { get { return DriverFactory.Driver; } }


        /// <summary>
        ///   Is there a field with a validation error message containing this text.
        /// </summary>
        /// <param name="contains"> Text to search for </param>
        /// <returns> True if found </returns>
        public bool HasFieldError(string contains)
        {
            var fieldValidationErrors = this.Driver.FindElements(By.ClassName("field-validation-error"));
            return fieldValidationErrors.Any(x => x.Text.Contains(contains));
        }

        /// <summary>
        ///   Does a validation summary error message exist and does it contain this text.
        /// </summary>
        /// <param name="contains"> Text to search for </param>
        /// <returns> True if found </returns>
        public bool HasFieldSummaryError(string contains)
        {
            try
            {
                var validationSummaryErrors = this.Driver.FindElement(By.ClassName("validation-summary-errors"));
                return validationSummaryErrors.Text.Contains(contains);
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool HasErrorSummary()
        {
            try
            {
                var validationSummaryErrors = this.Driver.FindElement(By.ClassName("validation-summary-errors"));
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

    }
}