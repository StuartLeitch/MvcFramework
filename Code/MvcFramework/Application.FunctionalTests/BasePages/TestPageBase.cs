using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.FunctionalTests.BasePages
{
    [TestClass]
    public abstract class TestPageBase<TPageOrControllerTest> : TestBase where TPageOrControllerTest : ControllerModel, new()
    {
        private TPageOrControllerTest _target;

        protected TPageOrControllerTest Target {
            get { return this._target; }
            set {
                if (value.GetType() != typeof(TPageOrControllerTest))
                    throw new ArgumentException("TestPage should only be set to the type that matches TPageTestOrControllerTest");

                this._target = value;
            }
        }
    }
}