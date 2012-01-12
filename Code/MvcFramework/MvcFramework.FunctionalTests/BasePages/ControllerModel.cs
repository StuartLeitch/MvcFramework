using System;
using System.Linq;
using MvcFramework.FunctionalTests.DeleporterHelpers;
using OpenQA.Selenium.Support.PageObjects;

namespace MvcFramework.FunctionalTests.BasePages
{
    /// <summary>
    ///   Class for Modeling a controller Use when testing at the Controller level (across multiple actions) While generally it is a better idea to test at the granularity of a page/view, there are times where it is more efficient to operate at the controller level if you don't have to setup PageModels.
    /// </summary>
    public class ControllerModel : ModelBase
    {
        public ControllerModel(string relativeUrlOfController) : base(relativeUrlOfController) {
            PageFactory.InitElements(DriverFactory.Driver, this);
        }

        /// <summary>
        ///   Is the browser currently on this page
        /// </summary>
        public virtual bool IsOnController { get { return this.Driver.Url.Contains(this.RelativeUrl); } }

        /// <summary>
        ///   Navigate to this action.
        /// </summary>
        public virtual void NavigateToAction(string actionUrl) {
            this.Driver.Navigate().GoToUrl(ServerAppUrl + this.RelativeUrl + "/" + actionUrl);
            this.ThrowIfServerError();
        }

        public void ThrowIfNotOnController() {
            if (!this.IsOnController)
                throw new InvalidOperationException("We are not currently on URL " + this.RelativeUrl
                                                    + " which is required to use this page method");
        }
    }
}