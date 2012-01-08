//using System;
//using System.Linq;
//using System.Web.Mvc;
//using Infrastructure.Core.TestHelpers;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using MvcFramework.Biz;
//using MvcFramework.Mvc.Controllers;

//namespace MvcFramework.Tests.Mvc
//{
//    [TestClass]
//    public class CallControllerTests : BaseControllerTest<CallController>
//    {
//        public CallControllerTests()
//            : base(MapperMaps.Initialize)
//        {
//        }

//        [TestMethod]
//        public void IndexGet_HasId_GetsCallLog()
//        {
//            var token = Guid.NewGuid();
//            this.Target.Index(token);
//            this.GetMock<ICallLogService>().Verify(x => x.GetCallLog(token), Times.Once());
//        }

//        [TestMethod]
//        public void IndexGet_NoId_DoesNotCallService()
//        {
//            var result = this.Target.Index((Guid?)null) as ViewResult;
//            var model = result.ViewData.Model as CallViewModel;
//            Assert.IsNotNull(model);
//            Assert.IsNull(model.Token);
//            this.GetMock<ICallLogService>().Verify(x => x.GetCallLog(It.IsAny<Guid>()), Times.Never());
//        }

//    }
//}