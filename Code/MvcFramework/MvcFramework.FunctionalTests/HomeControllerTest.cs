using System.Collections.Generic;
using System.Linq;
using DeleporterCore.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace MvcFramework.FunctionalTests
{
    [TestClass]
    public class HomeControllerTest : TestBase
    {
        [TestMethod]
        public void Index_MockCourses_MockWorks()
        {
            MockTestCourse();

            this.GoHome();

            var table = Driver.FindElements(By.CssSelector("table"));
            Assert.IsTrue(table.First().Text.Contains("Test1"));
        }

        [TestMethod]
        public void Test2()
        {
            MockTestCourse();

            this.GoHome();

            var table = Driver.FindElements(By.CssSelector("table"));
            Assert.IsTrue(table.First().Text.Contains("Test1"));
        }

        private static void MockTestCourse()
        {
            Deleporter.Run(() =>
                               {
                                   //var courses = new List<CourseListViewModel> { new CourseListViewModel { CourseTitle = "Test1", } };
                                   //var mockDateProvider = new Mock<ICourseService>();
                                   //mockDateProvider.Setup(x => x.GetCourses(CourseService.CourseType.A)).Returns(courses);

                                   //NinjectControllerFactoryUtils.TemporarilyReplaceBinding(mockDateProvider.Object);
                               });
        }

        private void GoHome()
        {
            Driver.Navigate().GoToUrl(ServerAppUrl);
        }
    }
}