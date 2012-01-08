using System.Linq;
using System;
using Infrastructure.Core.EventBroker.Interfaces;
using Infrastructure.Core.Membership;

namespace Infrastructure.Core.DependencyInjection
{
    // IMPROVE: Review if this needs updating in relation to changes (is there still a use case for this?).

    /// <summary>
    ///   The purpose of this class is to allow the use of Repositories and Services while not having a Controller inherit from BaseSecureController.
    ///   BaseRepository and BaseService both get an instance of EventBroker that is scoped to the request to get access to the user
    ///   and pass validation messages.  The validation events can be ignored and manually handled, but any code in repositories or services using 
    ///   the User property will fail if using a standard controller and not setting the SetUser method below.  Note that this can be set in the 
    ///   Controller constructor as it is a Func and execution is delayed until the User has been injected into the Controller by the MVC framework.
    /// </summary>
    public static class ManuallySetUserForRepositories
    {
        public static void SetUser(Func<IWrappedUser> functionToGetUser)
        {
            var generalBroker = ManuallyWiredDIContainer.Resolve<IEventBroker>();
            generalBroker.SetFunctionToGetWrappedUser(functionToGetUser);
        }
    }
}