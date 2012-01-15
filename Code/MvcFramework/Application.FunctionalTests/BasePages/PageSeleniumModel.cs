using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Application.FunctionalTests.DeleporterHelpers;
using OpenQA.Selenium.Support.PageObjects;

namespace Application.FunctionalTests.BasePages
{
    public abstract class PageSeleniumModel<TController> : SeleniumModel where TController : Controller
    {
        protected PageSeleniumModel(Expression<Func<TController, ActionResult>> action) {
            this.PageUrl = TestHelpers.UrlForAction(action);
            PageFactory.InitElements(DriverFactory.Driver, this);
        }

        /// <summary>
        ///   Is the browser currently on this page
        /// </summary>
        public virtual bool IsOnPage { get { return this.Driver.Url.Contains(this.PageUrl); } }

        /// <summary>
        ///   Url portion coming after http://localhost:1000/ for this page
        /// </summary>
        public string PageUrl { get; private set; }

        /// <summary>
        ///   Navigate to this page.
        /// </summary>
        public virtual void Navigate() {
            this.Driver.Navigate().GoToUrl(this.PageUrl);
            this.ThrowIfServerError();
            this.ThrowIfNotOnPage();
        }

        /// <summary>
        ///   Navigate to this page without verifying that we end up on the page.
        /// </summary>
        public virtual void NavigateWithoutVerify() {
            this.Driver.Navigate().GoToUrl(ServerAppUrl + this.PageUrl);
            this.ThrowIfServerError();
        }

        protected void ThrowIfNotOnPage() {
            if (!this.IsOnPage)
                throw new InvalidOperationException("We are not currently on URL " + this.PageUrl
                                                    + " which is required to use this page method");
        }
    }
}