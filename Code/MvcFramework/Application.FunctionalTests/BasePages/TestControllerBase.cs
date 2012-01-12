using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.FunctionalTests.BasePages
{
    /// <summary>
    ///   Use when testing at the Controller level (across multiple actions) While generally it is a better idea to test at the granularity of a page/view, there are times where it is more efficient to operate at the controller level.
    /// </summary>
    /// <typeparam name="TControllerModel"> Model representing the HTML pages of the controller to be tested </typeparam>
    /// <typeparam name="TControllerUnderTest"> The actual MVC controller being tested </typeparam>
    [TestClass]
    public abstract class TestControllerBase<TControllerModel, TControllerUnderTest> : TestBase where TControllerModel : ControllerModel
                                                                                                where TControllerUnderTest : Controller
    {
        /// <summary>
        ///   Handle on the Controller being tested. Pass this into NavigateToAction. Controller is null, so it will blow up if invoked (as designed - we're not testing the controllers directly here). Purpose is to get intellisense and compile time type checking.
        /// </summary>
        protected readonly TControllerUnderTest Controller;

        private TControllerModel _target;

        /// <summary>
        ///   A model of the controller under test
        /// </summary>
        protected TControllerModel Target {
            get { return this._target; }
            set {
                // It would be great to find a way to just call Activator.CreateInstance without the Selenium PageObject choking.
                // Area for possible future research - why does it choke? Are there workarounds?
                if (value.GetType() != typeof(TControllerModel))
                    throw new ArgumentException("ControllerModel should only be set to the type that matches TControllerModel");

                this._target = value;
            }
        }

        /// <summary>
        ///   Navigates to the action on the controller. Is intended to work in conjunction with this.Controller to provide intellisense and compile time type checking.
        /// </summary>
        /// <param name="expr"> () => this.Controller.ActionYouWantToCall </param>
        /// <param name="urlExtension"> (Optional) String to append to the URL after the Controller/Action/ </param>
        protected void NavigateToAction(Expression<Func<ActionResult>> expr, string urlExtension = "") {
            // Reflection overhead should be negligible in relationship to all the cross process stuff going on
            // when doing browser testing.
            var body = (MethodCallExpression)expr.Body;
            var actionName = body.Method.Name;

            this.Target.NavigateToAction(actionName + "/" + urlExtension);
        }
    }
}