using System.Linq;
using Application.Core.BaseClasses;
using Application.Core.Membership;

namespace System.Web.Mvc
{
    [Authorize]
    public abstract class BaseSecureController : BaseController
    {
        public IWrappedUser WrappedUser { get { return this.EventBroker.WrappedUser; } }

        protected override void SetEventBrokerUser() {
            // Set User so that Repositories and Services can have access to user
            // Needs to be a delegate because the Membership Provider is not available at this point in the request lifecycle.
            this._eventBroker.SetFunctionToGetWrappedUser(this.GetWrappedUser);
        }

        /// <summary>
        ///   Keep Private, provide access via EventBroker so it can be easily mocked.
        /// </summary>
        private WrappedUser GetWrappedUser() {
            var wrappedUser = this.HttpContext.User as WrappedUser;

            return wrappedUser;
        }
    }
}