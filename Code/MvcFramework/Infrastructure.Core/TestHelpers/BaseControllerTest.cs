using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Core.EventBroker.Interfaces;
using Moq;
using Ninject;

namespace Infrastructure.Core.TestHelpers
{
    public class BaseControllerTest<TClassUnderTest> : BaseTest<TClassUnderTest> where TClassUnderTest : BaseSecureController
    {
        public BaseControllerTest(Action MapperMaps_Initialize)
            : base(MapperMaps_Initialize)
        {
            this.Target.SetMockControllerContext();
           // this.Target.HttpContext.User = this.MockingKernel.Get<IBrokerFactory>().GetGeneralBroker().WrappedUser.User;

            //Mock.Get(this.Target.HttpContext).Setup(x => x.User).Returns(new GenericPrincipal(new GenericIdentity("sdf"),new string[]{Constants.RoleAdmin} ));
            var user = this.MockingKernel.Get<IBrokerFactory>().GetGeneralBroker().WrappedUser.User;
            Mock.Get(this.Target.HttpContext).Setup(x => x.User).Returns(user);

        }

        protected void SetUserRole(string role)
        {
            this.SetUser(1, "test", "test@test.com", new[] { role });
            var wrappedUser = this.MockingKernel.Get<IBrokerFactory>().GetGeneralBroker().WrappedUser;
            var user = wrappedUser.User;
            Mock.Get(this.Target.HttpContext).Setup(x => x.User).Returns(user);

        }
    }
}
