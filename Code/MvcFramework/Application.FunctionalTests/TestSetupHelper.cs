using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Application.Biz.Data.EntityFramework;
using Application.Core.Membership;
using Application.FunctionalTests.DeleporterHelpers;
using DeleporterCore.Client;
using Moq;
using MvcFramework.Mvc.Models;

namespace FrameworkTests
{
    public static class TestSetupHelper
    {
        public static void StubMembershipForTestUserLogin() {
            Deleporter.Run(() =>
            {
                var membershipMock = new Mock<IMembershipService>();
                membershipMock.Setup(x => x.ValidateUser("test", "password")).Returns(true);
                membershipMock.Setup(x => x.GetUser(It.IsAny<string>(), true)).Returns(new MembershipUser("AspNetSqlMembershipProvider", "Test", 1, "test@email.com", "", "",
                    true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now));

                NinjectUtils.TemporarilyReplaceBinding(membershipMock.Object);

                // Throw by default on data access
                var domainContextMock = new Mock<IDomainContext>(MockBehavior.Strict);
                NinjectUtils.TemporarilyReplaceBinding(domainContextMock.Object);
            });
        }

        public static void StubRegisterTestUser() {
            Deleporter.Run(() =>
            {
                Mock<IMembershipService> membershipMock = new Mock<IMembershipService>();
                membershipMock.Setup(x => x.CreateUser("test", "password", "test@user.com")).Returns(MembershipCreateStatus.Success);
                membershipMock.Setup(x => x.GetUser(It.IsAny<string>(), It.IsAny<bool>())).Returns(
                    new MembershipUser("AspNetSqlMembershipProvider", "Test", 1, "test@email.com", "", "",
                    true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now));

                NinjectUtils.TemporarilyReplaceBinding(membershipMock.Object);
            });
        }

        public static void ThrowOnNonMockedDataAccess() {
            Deleporter.Run(() =>
            {
                // Throw by default on data access
                var domainContextMock = new Mock<IDomainContext>(MockBehavior.Strict);
                NinjectUtils.TemporarilyReplaceBinding(domainContextMock.Object);
            });
        }
    }
}
