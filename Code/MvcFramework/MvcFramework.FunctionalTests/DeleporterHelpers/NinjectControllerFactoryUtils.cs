using System.Linq;

namespace MvcFramework.FunctionalTests.DeleporterHelpers
{
    public static class NinjectControllerFactoryUtils
    {
        // public static void TemporarilyReplaceBinding<TService>(TService implementation)
        // {
            // Uncomment the following if you are using Ninject as your IOC container

            //var controllerFactory = (NinjectControllerFactory)ControllerBuilder.Current.GetControllerFactory();
            //var kernel = controllerFactory.Kernel;

            //// Remove existing bindings and replace with new one
            //var originalBindings = kernel.GetBindings(typeof (TService)).ToList();
            //foreach (var originalBinding in originalBindings)
            //    kernel.RemoveBinding(originalBinding);
            //var replacementBinding = kernel.Bind<TService>().ToConstant(implementation).Binding;

            //// Clear up by doing the reverse
            //TidyupUtils.AddTidyupTask(() => {
            //    kernel.RemoveBinding(replacementBinding);
            //    foreach (var originalBinding in originalBindings)
            //        kernel.AddBinding(originalBinding);
            //});
        // }
    }
}