using System;
using System.Linq;
using System.Web.Security;

namespace MvcFramework.Mvc.Models
{
    public interface IMembershipService
    {
        int MinPasswordLength { get; }

        bool ChangePassword(string userName, string oldPassword, string newPassword);

        MembershipCreateStatus CreateUser(string userName, string password, string email);

        MembershipUser GetUser(object providerUserKey, bool userIsOnline);

        MembershipUser GetUser(string username, bool userIsOnline);

        void UpdateUser(MembershipUser user);

        bool ValidateUser(string userName, string password);
    }

    public class MembershipService : IMembershipService
    {
        private readonly MembershipProvider _provider;

        public MembershipService()
                : this(null)
        {
        }

        public MembershipService(MembershipProvider provider)
        {
            this._provider = provider ?? Membership.Provider;
        }

        public int MinPasswordLength
        {
            get
            {
                return this._provider.MinRequiredPasswordLength;
            }
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            if (String.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Value cannot be null or empty.", "userName");
            }
            if (String.IsNullOrEmpty(oldPassword))
            {
                throw new ArgumentException("Value cannot be null or empty.", "oldPassword");
            }
            if (String.IsNullOrEmpty(newPassword))
            {
                throw new ArgumentException("Value cannot be null or empty.", "newPassword");
            }

            // The underlying ChangePassword() will throw an exception rather
            // than return false in certain failure scenarios.
            try
            {
                var currentUser = this._provider.GetUser(userName, true /* userIsOnline */);
                return currentUser.ChangePassword(oldPassword, newPassword);
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            // if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Value cannot be null or empty.", "password");
            }
            if (String.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Value cannot be null or empty.", "email");
            }

            MembershipCreateStatus status;
            this._provider.CreateUser(userName, password, email, null, null, true, null, out status);
            return status;
        }

        public MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            return this._provider.GetUser(providerUserKey, userIsOnline);
        }

        public MembershipUser GetUser(string username, bool userIsOnline)
        {
            return this._provider.GetUser(username, userIsOnline);
        }

        public void UpdateUser(MembershipUser user)
        {
            this._provider.UpdateUser(user);
        }

        public bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Value cannot be null or empty.", "userName");
            }
            if (String.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Value cannot be null or empty.", "password");
            }

            return this._provider.ValidateUser(userName, password);
        }
    }
}