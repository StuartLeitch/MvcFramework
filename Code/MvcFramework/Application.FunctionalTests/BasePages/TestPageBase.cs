using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.FunctionalTests.BasePages
{
    [TestClass]
    public abstract class TestPageBase<TPageModel> : TestBase where TPageModel : PageModelBase, new()
    {
        private TPageModel _target;

        protected TPageModel Target {
            get { return this._target; }
            set {
                if (value.GetType() != typeof(TPageModel))
                    throw new ArgumentException("TestPage should only be set to the type that matches TPageTestOrControllerTest");

                this._target = value;
            }
        }
    }
}