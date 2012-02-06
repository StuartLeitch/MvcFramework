using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Application.Biz.Data;
using Application.Biz.Data.EntityFramework;
using Application.Core.EventBroker;
using Application.Core.EventBroker.Interfaces;
using Ninject;

namespace MvcFramework.Mvc
{
    public static class DIFactory
    {
        public static IKernel Kernel { get; private set; }

        /// <summary>
        /// This should be set in Mvc.App_Start.CreateKernel() after RegisterServices(kernel);
        /// </summary>
        /// <param name="kernel"></param>
        public static void SetKernalFromAppStart(IKernel kernel)
        {
            Kernel = kernel;
            SetRequestScopeForSpecialInterfaces();
        }

        /// <summary>
        /// Set the implementations of these interfaces to have one 
        /// instance shared across all uses within a request.
        /// </summary>
        private static void SetRequestScopeForSpecialInterfaces() {
            // EventBroker is used to auto-wire the controllers, services and repositories each request.
            // Therefore, we want Ninject to give us one instance of this per request.
            RemoveBinding(typeof(IEventBroker));
            Kernel.Bind<IEventBroker>().To<EventBroker>().InRequestScope();

            RemoveBinding(typeof(IGeneralRepository));
            Kernel.Bind<IGeneralRepository>().To<GeneralRepository>().InRequestScope();

            // Normally DomainContext will be created once per request via IGeneralRepository, 
            // but in case it is created independently, put InRequestScope.
            RemoveBinding(typeof(IDomainContext));
            Kernel.Bind<IDomainContext>().To<DomainContext>().InRequestScope().Named("RequestScope");

            // Contexts such as MembershipProviders and FilterProviders live beyond the request lifetime.
            Kernel.Bind<IDomainContext>().To<DomainContext>().InTransientScope().Named("Transient");
        }
        private static void RemoveBinding(Type type) {
            foreach (var binding in Kernel.GetBindings(type))
                Kernel.RemoveBinding(binding);
        }
        public static T Resolve<T>()
        {
            return Kernel.Get<T>();
        }
    }
}