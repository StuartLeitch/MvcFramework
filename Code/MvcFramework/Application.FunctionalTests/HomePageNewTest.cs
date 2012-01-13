using Application.FunctionalTests.BasePages;
using FrameworkTests.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.FunctionalTests
{
    [TestClass]
    public class HomePageNewTest : TestPageBase<HomePageModel>
    {

        [TestMethod]
        public void Paragraph_DisplaysMessage() {
            
        }



        protected override void OnTestRunInitialize() {
            Target = new HomePageModel();
            this.Target.Navigate();
        }

    }
}