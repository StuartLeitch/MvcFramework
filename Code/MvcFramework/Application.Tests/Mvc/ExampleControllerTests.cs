//using System;
//using System.Linq;
//using System.Web.Mvc;
//using Application.Core.TestHelpers;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using Application.Biz;
//using Application.Mvc.Controllers;

//namespace Application.Tests.Mvc
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