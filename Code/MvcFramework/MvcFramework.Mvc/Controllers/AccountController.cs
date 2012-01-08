using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using MvcFramework.Mvc.Models;

namespace MvcFramework.Mvc.Controllers
{
    public class AccountController : Controller
    {
        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }

        [Authorize]
        public ActionResult ChangePassword()
        {
            this.ViewBag.PasswordLength = this.MembershipService.MinPasswordLength;
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (this.ModelState.IsValid)
            {
                if (this.MembershipService.ChangePassword(this.User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return this.RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    this.ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            this.ViewBag.PasswordLength = this.MembershipService.MinPasswordLength;
            return this.View(model);
        }

        // **************************************
        // URL: /Account/ChangePasswordSuccess
        // **************************************

        public ActionResult ChangePasswordSuccess()
        {
            return this.View();
        }

        public ActionResult LogOff()
        {
            this.FormsService.SignOut();

            return this.RedirectToAction("Index", "Home");
        }

        // **************************************
        // URL: /Account/LogOn
        // **************************************

        public ActionResult LogOn()
        {
            if (this.Request.IsAjaxRequest())
            {
                return PartialView();
            }

            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (this.ModelState.IsValid)
            {
                if (this.MembershipService.ValidateUser(model.UserName, model.Password))
                {
                    this.LogUserIn(model.UserName, false);
                    return this.SafeRedirection(returnUrl);
                }
                else
                {
                    this.ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        // **************************************
        // URL: /Account/Register
        // **************************************

        public ActionResult Register()
        {
            this.ViewBag.PasswordLength = this.MembershipService.MinPasswordLength;
            if (this.Request.IsAjaxRequest())
            {
                return PartialView();
            }

            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (this.ModelState.IsValid)
            {
                // Attempt to register the user
                var createStatus = this.MembershipService.CreateUser(model.UserName, model.Password, model.Email);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    this.LogUserIn(model.UserName, false);
                    return this.RedirectToAction("Index", "Home");
                }
                else
                {
                    this.ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            this.ViewBag.PasswordLength = this.MembershipService.MinPasswordLength;
            return this.View(model);
        }

        protected override void Initialize(RequestContext requestContext)
        {
            if (this.FormsService == null)
            {
                this.FormsService = new FormsAuthenticationService();
            }
            if (this.MembershipService == null)
            {
                this.MembershipService = new AccountMembershipService();
            }

            base.Initialize(requestContext);
        }

        // **************************************
        // URL: /Account/ChangePassword
        // **************************************

        private void LogUserIn(string userName, bool createPersistentCookie)
        {
            var user = this.MembershipService.GetUser(userName, false);
            this.FormsService.SignIn(
                user.UserName, user.Email, int.Parse(user.ProviderUserKey.ToString()), createPersistentCookie, this.Response);
        }

        private ActionResult SafeRedirection(string redirectionUrl)
        {
            if (redirectionUrl == null || redirectionUrl == "/Account/PasswordResetSuccess")
            {
                return this.RedirectToAction("Index", "Home");
            }

            // IE does not provide leading slash, others do.
            if (!redirectionUrl.StartsWith("/"))
            {
                redirectionUrl = "/" + redirectionUrl;
            }
            if (this.Url.IsLocalUrl(redirectionUrl) && redirectionUrl.Length > 1 && redirectionUrl.StartsWith("/") &&
                !redirectionUrl.StartsWith("//") && !redirectionUrl.StartsWith("/\\"))
            {
                return this.Redirect(redirectionUrl);
            }

            if (redirectionUrl != "/")
            {
                // IMPROVE: If logging added, log this.
                // var message = string.Format("Open redirect to {0} detected.", redirectionUrl);
                //ErrorSignal.FromCurrentContext().Raise(new SecurityException(message));
            }

            return this.RedirectToAction("Index", "Home");
        }
    }
}