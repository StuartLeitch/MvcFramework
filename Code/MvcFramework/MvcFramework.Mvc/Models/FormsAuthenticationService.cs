using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using Application.Core;
using Application.Core.Membership;

namespace MvcFramework.Mvc.Models
{
    // The FormsAuthentication type is sealed and contains static members, so it is difficult to
    // unit test code that calls its members. The interface and helper class below demonstrate
    // how to create an abstract wrapper around such a type in order to make the AccountController
    // code unit testable.

    public interface IFormsAuthenticationService
    {
        void SignIn(string userName, string email, int userId, bool createPersistentCookie, HttpResponseBase response);

        void SignOut();
    }

    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        private void SetAuthTicket(string userName, string userData, HttpResponseBase response)
        {
            var ticket = new FormsAuthenticationTicket(
                    1, userName, DateTime.Now, DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes), true, userData);
            var encTicket = FormsAuthentication.Encrypt(ticket);
            var faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            response.Cookies.Add(faCookie);
        }

        public void SignIn(string userName, string email, int userId, bool createPersistentCookie, HttpResponseBase response)
        {
            email.ThrowIfNullOrEmpty("email");
            userName.ThrowIfNullOrEmpty("userName");
            userId.ThrowIfNonPositive("userId");

            var userData = UserDataParser.Encode(userId, userName, email);
            this.SetAuthTicket(userName, userData, response);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}