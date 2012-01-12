using System;
using System.Linq;
using MvcFramework.FunctionalTests.DeleporterHelpers;
using OpenQA.Selenium.Support.PageObjects;

namespace MvcFramework.FunctionalTests.BasePages
{
    public abstract class PageModelBase : ModelBase
    {
        protected PageModelBase(string relativeUrl) : base(relativeUrl) {
            PageFactory.InitElements(DriverFactory.Driver, this);
        }

        /// <summary>
        ///   Is the browser currently on this page
        /// </summary>
        public virtual bool IsOnPage { get { return this.Driver.Url.Contains(this.RelativeUrl); } }

        /// <summary>
        ///   Navigate to this page.
        /// </summary>
        public virtual void Navigate() {
            this.Driver.Navigate().GoToUrl(ServerAppUrl + this.RelativeUrl);
            this.ThrowIfServerError();
            this.ThrowIfNotOnPage();
        }

        protected void ThrowIfNotOnPage() {
            if (!this.IsOnPage)
                throw new InvalidOperationException("We are not currently on URL " + this.RelativeUrl
                                                    + " which is required to use this page method");
        }
    }
}