using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        }

        public static T Resolve<T>()
        {
            return Kernel.Get<T>();
        }
    }
}