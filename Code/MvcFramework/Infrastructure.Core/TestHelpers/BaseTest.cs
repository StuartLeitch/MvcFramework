using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using Infrastructure.Core.EventBroker.Interfaces;
using Infrastructure.Core.Membership;
using Moq;
using Ninject;
using Ninject.MockingKernel.Moq;
using Ninject.Modules;

namespace Infrastructure.Core.TestHelpers
{
    [DebuggerStepThrough]
    public abstract class BaseTest<TClassUnderTest> where TClassUnderTest : class
    {
        protected readonly MoqMockingKernel MockingKernel;

        /// <param name="MapperMaps_Initialize">Method to Initialize AutoMapper Maps</param>
        public BaseTest(Action MapperMaps_Initialize)
        {
            MapperMaps_Initialize.Invoke();

            // The only non mocked thing to return from the MockingKernel is our test version of the IBrokerFactory
            this.MockingKernel = new MoqMockingKernel(new NinjectSettings(), new TestModule());

            this.Target = MockingKernel.Get<TClassUnderTest>();

            this.SetUser(1, "TestUser", "Test@user.com", null);
        }

        /// <summary>
        ///   Object Under Test.
        /// </summary>
        protected TClassUnderTest Target { get; set; }

        protected void SetUser(int userId, string userName, string email, string[] roles = null)
        {
            var user = new WrappedUser(userId, userName, email, roles);

            this.MockingKernel.Get<IBrokerFactory>().GetGeneralBroker().SetFunctionToGetWrappedUser(() => user);
        }

        /// <summary>
        /// Gets the Mock object for the type
        /// Use this to setup mocks
        /// Scoped to thread
        /// </summary>
        /// <typeparam name="T">Interface of type to wrap in Mock object</typeparam>
        /// <returns>Mock object</returns>
        protected Mock<T> GetMock<T>() where T : class
        {
            return this.MockingKernel.GetMock<T>();
        }

        /// <summary>
        /// Gets the object instance within the generic mock 
        /// </summary>
        /// <returns>Instance of type T</returns>
        protected T Get<T>() where T : class
        {
            return this.GetMock<T>().Object;
        }

        protected T Get2<T>()
        {
            // IMPROVE: Test if Get and Get2 are the same.
            return this.MockingKernel.Get<T>();
        }

        // IMPROVE: Also test what happens when FlashHelpers fires.

        protected bool ValidationEventHasFired
        {
            get
            {
                return ((BrokerTestVersion)this.Get<IBrokerFactory>().GetGeneralBroker()).ValidationEventHasFired;
            }
        }

        private class TestModule : NinjectModule
        {
            public override void Load()
            {
                this.Bind<IBrokerFactory>().To<BrokerTestFactory>().InThreadScope();
            }
        }
    }
}