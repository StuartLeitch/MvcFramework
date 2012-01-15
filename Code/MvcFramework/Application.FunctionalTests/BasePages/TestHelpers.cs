using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using Application.FunctionalTests.DeleporterHelpers;
using DeleporterCore.Configuration;
using OpenQA.Selenium;

namespace Application.FunctionalTests.BasePages
{
    /// <summary>
    /// Various helpers that are used in both TestClasses and PageModelObjects
    /// </summary>
    internal static class TestHelpers
    {
        public static void ThrowIfServerError()
        {
            var elements = DriverFactory.Driver.FindElements(By.TagName("h1"));
            if (elements.Any(x => x.Text.Contains("Server Error")))
                throw new Exception("Server Error: \r\n" + DriverFactory.Driver.FindElement(By.TagName("title")).Text + "\r\n"
                                    + DriverFactory.Driver.PageSource);
        }

        private const string ServerAppBaseUrl = "http://localhost";

        /// <summary>
        ///   The URL for the base of the application ie. http://localhost:8080/
        /// </summary>
        public static string ServerAppUrl { get { return ServerAppBaseUrl + ":" + DeleporterConfiguration.WebHostPort + "/"; } }

        public static bool ElementExistsById(string id) {
            try {
                var test = DriverFactory.Driver.PageSource;
                DriverFactory.Driver.FindElement(By.Id(id));
            } catch (Exception exception) {
                Console.WriteLine(exception);
                return false;
            }
            return true;
            //var elements = DriverFactory.Driver.FindElements(By.Id(id));
            //return elements.Any();
        }

        public static string UrlForAction<TController>(Expression<Func<TController, ActionResult>> action) where TController : Controller
        {
            // Reflection overhead should be negligible in relationship to all the cross process stuff going on
            // when doing browser testing.
            var call = action.Body as MethodCallExpression;
            var name = typeof(TController).Name;
            var controllerName = name.Substring(0, name.Length - "Controller".Length);
            var actionName = call.Method.Name;

            return ServerAppUrl + controllerName + "/" + actionName;
        }

        public static void NavigateToAction<TController>(Expression<Func<TController, ActionResult>> action) where TController : Controller
        {
            var url = UrlForAction(action);
            DriverFactory.Driver.Navigate().GoToUrl(url);
            ThrowIfServerError();
        }


    }
}
