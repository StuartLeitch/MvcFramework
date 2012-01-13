using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeleporterCore.Client;

namespace FrameworkTests
{
    public static class TestSetupHelpers
    {
        public static void StubMembership() {
            Deleporter.Run(() =>
            {
                //var membershipMock = new Mock<IMembershipService>();
                //membershipMock.Setup(x => x.ValidateUser(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
                //membershipMock.Setup(x => x.GetUser(It.IsAny<string>())).Returns(new Person
                //{ UserName = "SomePerson", EmailAddress = "some@email.com", MemberID = 1 });
                //NinjectControllerFactoryUtils.TemporarilyReplaceBinding(membershipMock.Object);

                //// Throw by default on data access
                //var domainContextMock = new Mock<IDomainContext>(MockBehavior.Strict);
                //NinjectControllerFactoryUtils.TemporarilyReplaceBinding(domainContextMock.Object);
            });
        }

    }
}
