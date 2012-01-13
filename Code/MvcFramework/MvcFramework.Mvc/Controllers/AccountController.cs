using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using MvcFramework.Mvc.Models;

namespace MvcFramework.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private IFormsAuthenticationService _formsService;
        private IMembershipService _membershipService;

        public AccountController(IFormsAuthenticationService formsService, IMembershipService membershipService) {
            _formsService = formsService;
            _membershipService = membershipService;
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            this.ViewBag.PasswordLength = this._membershipService.MinPasswordLength;
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (this.ModelState.IsValid)
            {
                if (this._membershipService.ChangePassword(this.User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return this.RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    this.ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            this.ViewBag.PasswordLength = this._membershipService.MinPasswordLength;
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
            this._formsService.SignOut();

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
                if (this._membershipService.ValidateUser(model.UserName, model.Password))
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
            this.ViewBag.PasswordLength = this._membershipService.MinPasswordLength;
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
                var createStatus = this._membershipService.CreateUser(model.UserName, model.Password, model.Email);

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
            this.ViewBag.PasswordLength = this._membershipService.MinPasswordLength;
            return this.View(model);
        }

        // **************************************
        // URL: /Account/ChangePassword
        // **************************************

        private void LogUserIn(string userName, bool createPersistentCookie)
        {
            var user = this._membershipService.GetUser(userName, false);
            this._formsService.SignIn(
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