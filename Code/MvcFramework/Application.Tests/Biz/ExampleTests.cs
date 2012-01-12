//using System;
//using System.Collections.Generic;
//using System.Linq;
//using EntityExtensions;
//using Application.Core.TestHelpers;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using Application.Biz;
//using Application.Biz.Data;
//using Application.Biz.Data.EntityFramework;

//namespace Application.Tests.Biz
//{
//    [TestClass]
//    public class VisitServiceTests : BaseTest<VisitService>
//    {
//        public VisitServiceTests()
//            : base(MapperMaps.Initialize) {}

//        [TestMethod]
//        public void MoreThanTwoParticipants_None_ReturnsFalse()
//        {
//            var token = Guid.NewGuid();
//            this.GetMock<ILoadRepository>().Setup(x => x.GetPariticpants<Visit>(token)).Returns(
//                Enumerable.Empty<Participant>().AsQueryable());
//            var result = this.Target.MoreThanTwoParticipantsExist(token);
//            Assert.AreEqual(false, result);
//        }

//        [TestMethod]
//        public void UpsertVisit_HasToken_RetrievesRecord()
//        {
//            var token = Guid.NewGuid();
//            var model = new VisitViewModel { Token = token, };

//            this.GetMock<IGeneralRepository>().Setup(x => x.Db.Visits).Returns(new FakeDbSet<Visit>(new Visit { Token = token }));

//            this.Target.UpsertVisit(model, false);

//            this.GetMock<IGeneralRepository>().VerifyGet(x => x.Db.Visits, Times.Once());
//        }
//    }
//}