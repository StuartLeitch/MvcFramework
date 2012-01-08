using System.Linq;
using Infrastructure.Core.EventBroker.Interfaces;
using Ninject;
using Ninject.Modules;

namespace Infrastructure.Core.DependencyInjection
{
    /// <summary>
    ///   Because this is static, be careful when using this class in anything you want to Unit Test! See BrokerFactory for an example of how to wrap this class.
    /// </summary>
    public static class ManuallyWiredDIContainer
    {
        private static readonly IKernel Kernel = new StandardKernel(new GeneralBrokerModule());

        public static T Resolve<T>() {
            return Kernel.Get<T>();
        }
    }

    public class GeneralBrokerModule : NinjectModule
    {
        public override void Load() {
            // EventBroker is used to auto-wire the controllers, services and repositories each request.
            // Therefore, we want Ninject to give us one instance of this per request.
            this.Bind<IEventBroker>().To<EventBroker.EventBroker>().InRequestScope();
        }
    }
}