using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Application.FunctionalTests.DeleporterHelpers;
using OpenQA.Selenium;

namespace Application.FunctionalTests.BasePages
{
    /// <summary>
    ///   Typically inherit from PageSeleniumModel rather than use this directly (unless testing generic functionality that spans pages). Creates a Selenium model of a HTML page or pages
    /// </summary>
    public class SeleniumModel
    {
        private FlashModel _flash;
        private LayoutModel _layout;
        private ValidationModel _validation;

        public static string ServerAppUrl { get { return TestHelpers.ServerAppUrl; } }
        public IWebDriver Driver { get { return DriverFactory.Driver; } }
        public FlashModel Flash { get { return this._flash ?? (this._flash = new FlashModel()); } }
        public LayoutModel Layout { get { return this._layout ?? (this._layout = new LayoutModel()); } }
        public ValidationModel Validation { get { return this._validation ?? (this._validation = new ValidationModel()); } }

        public void ClearCookies() {
            this.Driver.Manage().Cookies.DeleteAllCookies();
        }

        public bool ElementExistsById(string id) {
            return TestHelpers.ElementExistsById(id);
        }

        public void NavigateToAction<TController>(Expression<Func<TController, ActionResult>> action) where TController : Controller {
            TestHelpers.NavigateToAction(action);
        }

        protected void ThrowIfServerError() {
            TestHelpers.ThrowIfServerError();
        }
    }
}