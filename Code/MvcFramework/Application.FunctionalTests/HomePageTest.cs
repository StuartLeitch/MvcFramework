using System.Collections.Generic;
using System.Linq;
using Application.FunctionalTests.BasePages;
using DeleporterCore.Client;
using FrameworkTests.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Application.FunctionalTests
{
    [TestClass]
    public class HomePageTest : TestPageBase<HomePageModel>
    {

        [TestMethod]
        public void TestWelcome_Present() {
            Assert.IsTrue(Target.Paragraph.Text.Contains("Put content here."));
        }



        private static void MockSomething()
        {
            var list = new Dictionary<string, int>();

            Deleporter.Run(() =>
                               {
                                   //var courses = new List<CourseListViewModel> { new CourseListViewModel { CourseTitle = "Test1", } };
                                   //var mockDateProvider = new Mock<ICourseService>();
                                   //mockDateProvider.Setup(x => x.GetCourses(CourseService.CourseType.A)).Returns(courses);

                                   //NinjectControllerFactoryUtils.TemporarilyReplaceBinding(mockDateProvider.Object);
                               });
        }


        protected override void OnTestRunInitialize()
        {
            Target = new HomePageModel();
            this.Target.Navigate();
        }
    }
}