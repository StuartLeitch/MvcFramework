using System.Linq;
using System.Web.Security;
using Infrastructure.Core.BaseClasses;
using Infrastructure.Core.Membership;

namespace System.Web.Mvc
{
    [Authorize]
    public abstract class BaseSecureController : BaseController
    {
        public IWrappedUser WrappedUser
        {
            get
            {
                return this.EventBroker.WrappedUser;
            }
        }

        protected override void SetEventBrokerUser()
        {
            // Set User so that Repositories and Services can have access to user
            // Needs to be a delegate because the Membership Provider is not available at this point in the request lifecycle.
            this._eventBroker.SetFunctionToGetWrappedUser(this.GetWrappedUser);
        }

        /// <summary>
        ///   Keep Private, provide access via EventBroker so it can be easily mocked.
        /// </summary>
        private WrappedUser GetWrappedUser()
        {
            var authCookie = this.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null || !this.User.Identity.IsAuthenticated)
                throw new InvalidOperationException("Attempted to get User, but User is not logged in.");

            var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

            var parsedUserData = authTicket.UserData.Split('|');

            int userId;
            if (parsedUserData.Length != 2 || !int.TryParse(parsedUserData[0], out userId))
                throw new InvalidOperationException("Attempted to get User, but data is corrupted.");

            var user = new WrappedUser(this.User) { UserId = userId, Email = parsedUserData[1] };

            return user;
        }
    }
}